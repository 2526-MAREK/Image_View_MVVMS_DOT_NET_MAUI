## @file PNGImage.py
#  @brief This module provides the PNGImage class for processing PNG images.

import os
import zlib
import struct

## @class PNGImage
#  @brief A class for processing PNG images.

class PNGImage:
    def __init__(self):
        pass

    ## @brief Reads a chunk from a PNG file.
    #  @param file The file object to read the chunk from.
    #  @return A tuple containing the chunk name and the chunk data.
    #  @exception ValueError Raised if the CRC check fails.
    @staticmethod
    def get_chunk(file):
        length = int.from_bytes(file.read(4), byteorder='big')  # Read chunk length
        chunk_name = file.read(4).decode('ascii')  # Read chunk name`
        chunk_data = file.read(length)  # Read chunk data
        crc = int.from_bytes(file.read(4), byteorder='big')  # Read CRC (sum of control)
        if crc != zlib.crc32(chunk_name.encode('ascii') + chunk_data):  # Check CRC
            raise ValueError('Incorrect sum of control')

        return chunk_name, chunk_data


    ## @brief Deletes output files from the specified folders.
    #  @param output_folder The folder containing output JSON files.
    #  @param output_folder_img The folder containing output image files.
    @staticmethod
    def delete_output_files(output_folder, output_folder_img):
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
        if os.path.exists(output_folder + "gAMA.json"):
            os.remove(output_folder + "gAMA.json")
        if os.path.exists(output_folder + "iTXt.json"):
            os.remove(output_folder + "iTXt.json")
        if os.path.exists(output_folder + "sPLT.json"):
            os.remove(output_folder + "sPLT.json")
        if os.path.exists(output_folder + "hIST.json"):
            os.remove(output_folder + "hIST.json")
        if os.path.exists(output_folder + "sTER.json"):
            os.remove(output_folder + "sTER.json")
        if os.path.exists(output_folder + "sRGB.json"):
            os.remove(output_folder + "sRGB.json")
        if os.path.exists(output_folder + "sBIT.json"):
            os.remove(output_folder + "sBIT.json")
        if os.path.exists(output_folder + "oFFs.json"):
            os.remove(output_folder + "oFFs.json")
        if os.path.exists(output_folder_img + "hist.png"):
            os.remove(output_folder_img + "hist.png")
        if os.path.exists(output_folder_img + "hist_rgb.png"):
            os.remove(output_folder_img + "hist_rgb.png")
        if os.path.exists(output_folder_img + "thumbnail_output.png"):
            os.remove(output_folder_img + "thumbnail_output.png")
        if os.path.exists(output_folder_img + "fft.png"):
            os.remove(output_folder_img + "fft.png")


    ## @brief Removes redundant chunks from the input PNG file and saves the result to the output file.
    #  @param input_file_path The path to the input PNG file.
    #  @param output_file_path The path to the output PNG file.
    @staticmethod
    def delete_redundant_chunks(input_file_path, output_file_path):
        essential_chunks = {'IHDR', 'IDAT', 'IEND'}

        with open(input_file_path, 'rb') as input_file, open(output_file_path, 'wb') as output_file:
            # Copy PNG signature
            signature = input_file.read(8)
            output_file.write(signature)

            while True:
                try:
                    chunk_name, chunk_data = PNGImage.get_chunk(input_file)
                except ValueError:
                    break

                # Write essential chunks to the output file
                if chunk_name in essential_chunks:
                    output_file.write(len(chunk_data).to_bytes(4, byteorder='big'))
                    output_file.write(chunk_name.encode('ascii'))
                    output_file.write(chunk_data)
                    crc = zlib.crc32(chunk_name.encode('ascii') + chunk_data)
                    output_file.write(crc.to_bytes(4, byteorder='big'))

                if chunk_name == 'IEND':
                    break

    ## @brief Removes unwanted chunks from the input PNG file and saves the result to the output file.
    #  @param png_file The path to the input PNG file.
    #  @param output_file The path to the output PNG file.
    @staticmethod
    def remove_unwanted_chunks(png_file, output_file):
        with open(png_file, 'rb') as src, open(output_file, 'wb') as dest:
            dest.write(src.read(8))  # Write PNG signature

            while True:
                chunk_data = src.read(8)
                if len(chunk_data) < 8:
                    break

                length, chunk_type = struct.unpack('!I4s', chunk_data)
                chunk_data = src.read(length + 4)  # Read chunk data and CRC

                if chunk_type not in (b'IDAT', b'IHDR', b'IEND', b'PLTE', b'tRNS', b'gAMA', b'sRGB', b'cHRM', b'iCCP', b'bKGD', b'hIST', b'pHYs', b'sBIT', b'tIME', b'tEXt', b'zTXt', b'iTXt', b'vpAg', b'sTER'):
                    continue  # Skip unwanted chunks

                dest.write(struct.pack('!I4s', length, chunk_type))
                dest.write(chunk_data)