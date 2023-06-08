# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
#                                                               #
#     Napisz flage jeśli chcesz ustawić tą wartość na True:     #
#                                                               #
#     Flagi:                                                    #
#   --windows      - jeśli uruchamiamy na Windowsie             #
#   --draw_fft     - rysuje FFT                                 #
#   --draw_hist    - rysuje histogram                           #
#   --print_info   - wypisuje informacje o pliku                #
#   --clear_chunks - usuwa metadane obrazu, aktualizuje jsony   #
#                                                               #
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #

from png_handler import PNGImage
from chunk_parser import ChunkParser
from image_cleaner import PNGImageCleaner
from image_analysis import ImageAnalysis
import argparse
import os


def main(args):
    Windows    = args.windows

    draw_fft = args.draw_fft
    draw_hist = args.draw_hist
    print_info = args.print_info
    clear_chunks = args.clear_chunks

    if Windows:
        file_name = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Model\\PythonScripts\\photo_processed.png"
        output_json_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\python_output\\"
        output_imgs_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Images\\python_output\\"
    else:
        file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/photo_processed.png"
        output_json_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Raw/"
        output_imgs_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Images/"

    # Only for debugging:
    # file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/ExampleImages/gAMA.png"

    file_name = "/Users/erykwojcik/Desktop/cleaned.png"

    hIST = None
    PNGImage.delete_output_files(output_json_folder, output_imgs_folder)

    if clear_chunks:
        output_cleaned_png = os.path.join(output_imgs_folder, "cleaned.png")
        PNGImage.delete_redundant_chunks(file_name, output_cleaned_png)

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


    # cleaner = PNGImageCleaner
    # cleaner.remove_text_chunks(file_name, 'cleaned.png')
    # PNGImage.delete_redundant_chunks(file_name, (output_json_folder + 'png_no_meta.png'))


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description="Obsługa argumentów wywołania")
    parser.add_argument("--windows", action="store_true", help="Ustaw, jeśli używasz systemu Windows")
    parser.add_argument("--draw_fft", action="store_true", help="Rysuj FFT obrazu")
    parser.add_argument("--draw_hist", action="store_true", default=True, help="Rysuj histogram obrazu (domyślnie: True)")
    parser.add_argument("--print_info", action="store_true", default=True, help="Wyświetl informacje o chunkach")
    parser.add_argument("--clear_chunks", action="store_true", help="Usuwa metadane obrazu, aktualizuje jsony")

    args = parser.parse_args()
    main(args)