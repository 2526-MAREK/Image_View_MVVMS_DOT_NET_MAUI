import os
import zlib

class PNGImage:
    def __init__(self):
        pass

    @staticmethod
    def get_chunk(file):
        length = int.from_bytes(file.read(4), byteorder='big')  # Read chunk length
        chunk_name = file.read(4).decode('ascii')  # Read chunk name`
        chunk_data = file.read(length)  # Read chunk data
        crc = int.from_bytes(file.read(4), byteorder='big')  # Read CRC (sum of control)
        if crc != zlib.crc32(chunk_name.encode('ascii') + chunk_data):  # Check CRC
            raise ValueError('Incorrect sum of control')

        return chunk_name, chunk_data


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