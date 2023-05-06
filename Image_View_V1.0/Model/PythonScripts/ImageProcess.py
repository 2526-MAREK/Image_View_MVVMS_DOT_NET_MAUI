## @file ImageProcess.py
# @brief This is the main script of the Image Analysis and Processing module.
"""
Image Analysis and Processing Script

This script provides functionality to analyze and process PNG images. Available options include:
- Running the script on Windows
- Drawing Fast Fourier Transform (FFT)
- Drawing histogram
- Printing information about the image file
- Clearing metadata chunks from the image and updating JSON files

Usage:
    Run the script with desired flags to enable specific features.
    Flags:
        --windows      - if running on Windows
        --draw_fft     - draw FFT
        --draw_hist    - draw histogram
        --print_info   - print information about the file
        --clear_chunks - remove image metadata and update JSON files
"""

from png_handler import PNGImage
from chunk_parser import ChunkParser
from image_analysis import ImageAnalysis
import argparse
import os


Windows    = True
draw_fft = False
draw_hist = False
print_info = False
clear_chunks = False

if Windows:
    main_path = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\"
    file_name = main_path + "Images\\photo_processed.png"
    output_json_folder = main_path + "Raw\\"
    output_imgs_folder = main_path + "Images\\"
else:
    file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/photo_processed.png"
    output_json_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Raw/"
    output_imgs_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Images/"

# Only for debugging:
# file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/ExampleImages/gAMA.png"

hIST = None
PNGImage.delete_output_files(output_json_folder, output_imgs_folder)

if clear_chunks:
    output_cleaned_png = os.path.join(output_imgs_folder, "cleaned.png")
    PNGImage.remove_unwanted_chunks(file_name, output_cleaned_png)

with open(file_name, 'rb') as file:     # Open PNG file
    file.read(8)                        # Read PNG file header
    img_data = b''                      # Variable storing image data

    while True:  # Read all chunks
        chunk_name, chunk_data = PNGImage.get_chunk(file)

        if chunk_name == 'IDAT':
            img_data += chunk_data
        elif chunk_name == 'hIST':
            hIST = ChunkParser.get_hIST_data(chunk_data)
        elif chunk_name == 'IEND':
            break

        if print_info:
            print(f'Chunk name: {chunk_name}')
            print(f'Chunk length: {len(chunk_data)}')
            print(f'Data: {chunk_data.hex()}')
            print(ChunkParser.get_chunk_data(chunk_name, chunk_data))

        ChunkParser.save_chunk_data_to_json(chunk_name, chunk_data, output_json_folder)

ImageAnalysis.create_thumbnail(file_name, output_imgs_folder, (128, 128))
ImageAnalysis.fft_of_image(file_name, output_imgs_folder, draw_fft)
ImageAnalysis.histogram_of_image(file_name, output_json_folder, output_imgs_folder, draw_hist, hIST)