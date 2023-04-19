import matplotlib.pyplot as plt
import numpy as np
import zlib
import json
import cv2  # Only for histogram drawing
import os   # For deleting files
import time

def get_chunk(file):
    length = int.from_bytes(file.read(4), byteorder='big')  # Read chunk length
    chunk_name = file.read(4).decode('ascii')  # Read chunk name`
    chunk_data = file.read(length)  # Read chunk data
    crc = int.from_bytes(file.read(4), byteorder='big')  # Read CRC (sum of control)
    if crc != zlib.crc32(chunk_name.encode('ascii') + chunk_data):  # Check CRC
        raise ValueError('Incorrect sum of control')

    return chunk_name, chunk_data


def get_IHDR_data(chunk_data):
    width = int.from_bytes(chunk_data[0:4], byteorder='big')
    height = int.from_bytes(chunk_data[4:8], byteorder='big')
    bit_depth = int.from_bytes(chunk_data[8:9], byteorder='big')
    color_type = int.from_bytes(chunk_data[9:10], byteorder='big')
    compression_method = int.from_bytes(chunk_data[10:11], byteorder='big')
    filter_method = int.from_bytes(chunk_data[11:12], byteorder='big')
    interlace_method = int.from_bytes(chunk_data[12:13], byteorder='big')
    return {'width': width,
            'height': height,
            'bit_depth': bit_depth,
            'color_type': color_type,
            'compression_method': compression_method,
            'filter_method': filter_method,
            'interlace_method': interlace_method}


def get_tEXt_data(chunk_data):
    null_byte_index = chunk_data.find(b'\x00')
    keyword = chunk_data[:null_byte_index].decode('utf-8')
    text = chunk_data[null_byte_index + 1:].decode('utf-8')
    return {'Keyword': keyword,
            'Text': text}


def get_tIME_data(chunk_data):
    year = int.from_bytes(chunk_data[:2], byteorder='big')
    month = int.from_bytes(chunk_data[2:3], byteorder='big')
    day = int.from_bytes(chunk_data[3:4], byteorder='big')
    hour = int.from_bytes(chunk_data[4:5], byteorder='big')
    minute = int.from_bytes(chunk_data[5:6], byteorder='big')
    second = int.from_bytes(chunk_data[6:7], byteorder='big')
    return {'Year': year,
            'Month': month,
            'Day': day,
            'Hour': hour,
            'Minute': minute,
            'Second': second}


def get_pHYs_data(chunk_data):
    pixels_per_unit_x = int.from_bytes(chunk_data[:4], byteorder='big')
    pixels_per_unit_y = int.from_bytes(chunk_data[4:8], byteorder='big')
    unit_specifier = chunk_data[8]
    return {'PixelsPerUnitX': pixels_per_unit_x,
            'PixelsPerUnitY': pixels_per_unit_y,
            'UnitSpecifier': unit_specifier}


def get_chunk_data(chunk_name, chunk_data):
    if chunk_name == 'IHDR':
        return get_IHDR_data(chunk_data)
    elif chunk_name == 'tEXt':
        return get_tEXt_data(chunk_data)
    elif chunk_name == 'tIME':
        return get_tIME_data(chunk_data)
    elif chunk_name == 'pHYs':
        return get_pHYs_data(chunk_data)
    else:
        return None


def save_chunk_data_to_json(chunk_name, chunk_data, output_folder):
    if chunk_name == 'IHDR':
        with open(output_folder + 'IHDR.json', 'w') as f:
            json.dump(get_IHDR_data(chunk_data), f)
    elif chunk_name == 'tEXt':
        with open(output_folder + 'tEXt.json', 'w') as f:
            json.dump(get_tEXt_data(chunk_data), f)
    elif chunk_name == 'tIME':
        with open(output_folder + 'tIME.json', 'w') as f:
            json.dump(get_tIME_data(chunk_data), f)
    elif chunk_name == 'pHYs':
        with open(output_folder + 'pHYs.json', 'w') as f:
            json.dump(get_pHYs_data(chunk_data), f)
    else:
        return None


def delete_output_files(output_folder):
    if os.path.exists(output_folder + "IHDR.json"):
        os.remove(output_folder + "IHDR.json")
    if os.path.exists(output_folder + "tEXt.json"):
        os.remove(output_folder + "tEXt.json")
    if os.path.exists(output_folder + "tIME.json"):
        os.remove(output_folder + "tIME.json")
    if os.path.exists(output_folder + "pHYs.json"):
        os.remove(output_folder + "pHYs.json")
    if os.path.exists(output_folder + "hist.json"):
        os.remove(output_folder + "hist.json")
    if os.path.exists(output_folder + "hist_r.json"):
        os.remove(output_folder + "hist_r.json")
    if os.path.exists(output_folder + "hist_g.json"):
        os.remove(output_folder + "hist_g.json")
    if os.path.exists(output_folder + "hist_b.json"):
        os.remove(output_folder + "hist_b.json")
    if os.path.exists(output_folder + "fft.png"):
        os.remove(output_folder + "fft.png")


def fft_of_image(image_path, output_path, draw_plots):

    img = cv2.imread(image_path, cv2.IMREAD_GRAYSCALE)

    f = np.fft.fft2(img)
    f_shift = np.fft.fftshift(f)
    magnitude_spectrum = 20 * np.log(np.abs(f_shift))

    if draw_plots:                                  # Display the input image and magnitude spectrum
        plt.subplot(121), plt.imshow(img, cmap='gray')
        plt.title('Input Image'), plt.xticks([]), plt.yticks([])
        plt.subplot(122), plt.imshow(magnitude_spectrum, cmap='gray')
        plt.title('Magnitude Spectrum'), plt.xticks([]), plt.yticks([])
        plt.show()

    
    cv2.imwrite(output_path + 'fft.png', magnitude_spectrum)    # Save the magnitude spectrum to a file


def histogram_of_image(image_path, output_folder, draw_plots):
    # Load the image
    img = cv2.imread(image_path)
    img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
    img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

    # Compute histograms
    hist_gray = cv2.calcHist([img_gray], [0], None, [256], [0, 256])
    hist_r = cv2.calcHist([img_rgb], [0], None, [256], [0, 256])
    hist_g = cv2.calcHist([img_rgb], [1], None, [256], [0, 256])
    hist_b = cv2.calcHist([img_rgb], [2], None, [256], [0, 256])

    if draw_plots:
        plt.figure()
        plt.subplot(221), plt.plot(hist_gray), plt.title('Grayscale Histogram')
        plt.subplot(222), plt.plot(hist_r, color='r'), plt.title('Red Channel Histogram')
        plt.subplot(223), plt.plot(hist_g, color='g'), plt.title('Green Channel Histogram')
        plt.subplot(224), plt.plot(hist_b, color='b'), plt.title('Blue Channel Histogram')
        plt.tight_layout()
        plt.show()

    with open(output_folder + 'hist.json', 'w') as f:
        json.dump(hist_gray.tolist(), f)
    with open(output_folder + 'hist_r.json', 'a') as f:
        json.dump(hist_r.tolist(), f)
    with open(output_folder + 'hist_g.json', 'a') as f:
        json.dump(hist_g.tolist(), f)
    with open(output_folder + 'hist_b.json', 'a') as f:
        json.dump(hist_b.tolist(), f)


# Initial conditions
draw_plots = True
Windows = False

if Windows:
    file_name = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Model\\PythonScripts\\photo_processed.png"
    output_json_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Raw\\python_output\\"
    output_imgs_folder = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\Images\\python_output\\"
else :
    file_name = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Model/PythonScripts/photo_processed.png"
    output_json_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Raw/"
    output_imgs_folder = "/Users/erykwojcik/Documents/GitHub/Image_View_MVVC/Image_View_V1.0/Resources/Images/"


delete_output_files(output_json_folder)
delete_output_files(output_imgs_folder)


with open(file_name, 'rb') as file:  # Open PNG file
    file.read(8) # Read PNG file header
    img_data = b''  # Variable storing image data

    while True:  # Read all chunks
        chunk_name, chunk_data = get_chunk(file)
        #print(f'Chunk name: {chunk_name}')  # Display chunk info
        #print(f'Chunk length: {len(chunk_data)}')
        #print(f'Data: {chunk_data.hex()}')

        if chunk_name == 'IDAT':
            img_data += chunk_data
        elif chunk_name == 'IEND':
            break
        else:
            #print(get_chunk_data(chunk_name, chunk_data))
            save_chunk_data_to_json(chunk_name, chunk_data, output_json_folder)

# img_bytes = zlib.decompress(img_data)  # Decompress image data
# img = np.frombuffer(img_bytes, dtype=np.uint8).reshape(-1)


fft_of_image(file_name, output_imgs_folder, draw_plots)
histogram_of_image(file_name, output_json_folder, draw_plots)
