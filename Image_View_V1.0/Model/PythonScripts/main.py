from png_handler import PNGImage
from chunk_parser import ChunkParser
from image_analysis import ImageAnalysis


draw_plots = True
Windows = False
print_info = False

if Windows:
    file_name = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Model\\PythonScripts\\photo_processed.png"
    output_json_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\python_output\\"
    output_imgs_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Images\\python_output\\"
else:
    file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/photo_processed.png"
    output_json_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Raw/"
    output_imgs_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Images/"

# Only for debugging:
# file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/ExampleImages/sRGB.png"



PNGImage.delete_output_files(output_json_folder)
PNGImage.delete_output_files(output_imgs_folder)


with open(file_name, 'rb') as file:  # Open PNG file
    file.read(8) # Read PNG file header
    img_data = b''  # Variable storing image data

    while True:  # Read all chunks
        chunk_name, chunk_data = PNGImage.get_chunk(file)
        if print_info:
            print(f'Chunk name: {chunk_name}')  # Display chunk info
            print(f'Chunk length: {len(chunk_data)}')
            print(f'Data: {chunk_data.hex()}')

        if chunk_name == 'IDAT':
            img_data += chunk_data
        elif chunk_name == 'IEND':
            break
        else:
            if print_info:
                print(ChunkParser.get_chunk_data(chunk_name, chunk_data))
            ChunkParser.save_chunk_data_to_json(chunk_name, chunk_data, output_json_folder)

# img_bytes = zlib.decompress(img_data)  # Decompress image data
# img = np.frombuffer(img_bytes, dtype=np.uint8).reshape(-1)


ImageAnalysis.fft_of_image(file_name, output_imgs_folder, draw_plots)
ImageAnalysis.histogram_of_image(file_name, output_json_folder, output_imgs_folder, draw_plots)
