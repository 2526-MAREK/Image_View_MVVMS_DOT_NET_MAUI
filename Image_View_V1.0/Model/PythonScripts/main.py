from png_handler import PNGImage
from chunk_parser import ChunkParser
from image_analysis import ImageAnalysis

Windows = False

draw_fft = True
draw_hist = True
hist_rgb = False
print_info = True

if Windows:
    file_name = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Model\\PythonScripts\\photo_processed.png"
    output_json_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\python_output\\"
    output_imgs_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Images\\python_output\\"
else:
    file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/photo_processed.png"
    output_json_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Raw/"
    output_imgs_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Images/"

# Only for debugging:
# file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/ExampleImages/hIST.png"
#
PNGImage.delete_output_files(output_json_folder)
PNGImage.delete_output_files(output_imgs_folder)

hIST = None

with open(file_name, 'rb') as file:     # Open PNG file
    file.read(8)                        # Read PNG file header
    img_data = b''                      # Variable storing image data

    while True:  # Read all chunks
        chunk_name, chunk_data = PNGImage.get_chunk(file)

        if chunk_name == 'IDAT':
            img_data += chunk_data
        elif chunk_name == 'hIST':
            hIST = ChunkParser.save_hIST_data(chunk_data)
        elif chunk_name == 'IEND':
            break

        if print_info:
            print(f'Chunk name: {chunk_name}')
            print(f'Chunk length: {len(chunk_data)}')
            print(f'Data: {chunk_data.hex()}')
            print(ChunkParser.get_chunk_data(chunk_name, chunk_data))

        ChunkParser.save_chunk_data_to_json(chunk_name, chunk_data, output_json_folder)



ImageAnalysis.fft_of_image(file_name, output_imgs_folder, draw_fft)
ImageAnalysis.histogram_of_image(file_name, output_json_folder, output_imgs_folder, draw_hist, hist_rgb, hIST)


PNGImage.delete_redundant_chunks(file_name, (output_json_folder + 'png_no_meta.png'))