from png_handler import PNGImage
import zlib


class PNGImageCleaner():


    def remove_text_chunks(self, input_file, output_file):

        with open(input_file, 'rb') as f_in, open(output_file, 'wb') as f_out:
            # Read PNG signature
            png_signature = f_in.read(8)
            if png_signature != b'\x89PNG\r\n\x1a\n':
                raise ValueError('Not a valid PNG file')
            f_out.write(png_signature)

            while True:
                chunk_name, chunk_data = PNGImage.get_chunk(f_in)
                print(chunk_name)

                if chunk_name == 'IEND':
                    # Write IEND chunk and break the loop

                    break

                if chunk_name in {'tEXt', 'zTXt', 'iTXt'}:
                    # Ignore these chunks
                    continue

                # Write chunk to output file
                chunk_length = len(chunk_data)
                f_out.write(chunk_length.to_bytes(4, byteorder='big'))
                f_out.write(chunk_name.encode('ascii'))
                f_out.write(chunk_data)
                f_out.write(zlib.crc32(chunk_name.encode('ascii') + chunk_data).to_bytes(4, byteorder='big'))



# Only for debugging:
output_path = "/Users/erykwojcik/Desktop/"
file_name = "/Users/erykwojcik/Desktop/ExampleImages/doge.png"
   
cleaner = PNGImageCleaner()
# cleaner.remove_text_chunks(file_name, output_path + 'cleaned.png')
cleaner.remove_text_chunks(output_path + 'cleaned.png', output_path + 'cleaned.png')