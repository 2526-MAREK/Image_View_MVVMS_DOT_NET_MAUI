import json


class ChunkParser:

    def __init__(self):
        pass

    @staticmethod
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

    @staticmethod
    def get_tEXt_data(chunk_data):
        null_byte_index = chunk_data.find(b'\x00')
        keyword = chunk_data[:null_byte_index].decode('utf-8')
        text = chunk_data[null_byte_index + 1:].decode('utf-8')
        return {'Keyword': keyword,
                'Text': text}

    @staticmethod
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

    @staticmethod
    def get_pHYs_data(chunk_data):
        pixels_per_unit_x = int.from_bytes(chunk_data[:4], byteorder='big')
        pixels_per_unit_y = int.from_bytes(chunk_data[4:8], byteorder='big')
        unit_specifier = chunk_data[8]
        return {'PixelsPerUnitX': pixels_per_unit_x,
                'PixelsPerUnitY': pixels_per_unit_y,
                'UnitSpecifier': unit_specifier}

    @staticmethod
    def get_gAMA_data(chunk_data):
        gamma = int.from_bytes(chunk_data, byteorder='big')
        return {'Gamma': gamma}

    @staticmethod
    def get_hIST_data(chunk_data):
        hist_values = [int.from_bytes(chunk_data[i:i + 2], byteorder='big') for i in range(0, len(chunk_data), 2)]
        return {'Histogram': hist_values}

    @staticmethod
    def get_iTXt_data(chunk_data):
        null_byte_index = chunk_data.find(b'\x00')
        keyword = chunk_data[:null_byte_index].decode('utf-8')
        compression_flag = chunk_data[null_byte_index + 1]
        compression_method = chunk_data[null_byte_index + 2]
        null_byte_index2 = chunk_data.find(b'\x00', null_byte_index + 3)
        language_tag = chunk_data[null_byte_index + 3:null_byte_index2].decode('utf-8')
        null_byte_index3 = chunk_data.find(b'\x00', null_byte_index2 + 1)
        translated_keyword = chunk_data[null_byte_index2 + 1:null_byte_index3].decode('utf-8')
        text = chunk_data[null_byte_index3 + 1:].decode('utf-8')

        return {
            'Keyword': keyword,
            'CompressionFlag': compression_flag,
            'CompressionMethod': compression_method,
            'LanguageTag': language_tag,
            'TranslatedKeyword': translated_keyword,
            'Text': text
        }

    @staticmethod
    def get_sPLT_data(chunk_data):
        null_byte_index = chunk_data.find(b'\x00')
        palette_name = chunk_data[:null_byte_index].decode('utf-8')
        sample_depth = chunk_data[null_byte_index + 1]
        entries = []
        entry_size = 6 if sample_depth == 8 else 10
        for i in range(null_byte_index + 2, len(chunk_data), entry_size):
            red = int.from_bytes(chunk_data[i:i + 2], byteorder='big') if sample_depth == 16 else chunk_data[i]
            green = int.from_bytes(chunk_data[i + 2:i + 4], byteorder='big') if sample_depth == 16 else chunk_data[
                i + 1]
            blue = int.from_bytes(chunk_data[i + 4:i + 6], byteorder='big') if sample_depth == 16 else chunk_data[i + 2]
            alpha = int.from_bytes(chunk_data[i + 6:i + 8], byteorder='big') if sample_depth == 16 else chunk_data[
                i + 3]
            frequency = int.from_bytes(chunk_data[i + 8:i + 10], byteorder='big') if sample_depth == 16 else chunk_data[
                i + 4]
            entries.append({
                'Red': red,
                'Green': green,
                'Blue': blue,
                'Alpha': alpha,
                'Frequency': frequency
            })

        return {
            'PaletteName': palette_name,
            'SampleDepth': sample_depth,
            'Entries': entries
        }

    @staticmethod
    def get_sTER_data(chunk_data):
        stereo_mode = chunk_data[0]
        return {'StereoMode': stereo_mode}

    @staticmethod
    def get_sRGB_data(chunk_data):
        rendering_intent = chunk_data[0]
        return {'RenderingIntent': rendering_intent}

    @staticmethod
    def get_oFFs_data(chunk_data):
        position_x = int.from_bytes(chunk_data[:4], byteorder='big', signed=True)
        position_y = int.from_bytes(chunk_data[4:8], byteorder='big', signed=True)
        unit_specifier = chunk_data[8]
        return {'PositionX': position_x,
                'PositionY': position_y,
                'UnitSpecifier': unit_specifier}

    @staticmethod
    def get_chunk_data(chunk_name, chunk_data):
        if chunk_name == 'IHDR':
            return ChunkParser.get_IHDR_data(chunk_data)
        elif chunk_name == 'tEXt':
            return ChunkParser.get_tEXt_data(chunk_data)
        elif chunk_name == 'tIME':
            return ChunkParser.get_tIME_data(chunk_data)
        elif chunk_name == 'pHYs':
            return ChunkParser.get_pHYs_data(chunk_data)
        elif chunk_name == 'gAMA':
            return ChunkParser.get_gAMA_data(chunk_data)
        elif chunk_name == 'hIST':
            return ChunkParser.get_hIST_data(chunk_data)
        elif chunk_name == 'iTXt':
            return ChunkParser.get_iTXt_data(chunk_data)
        elif chunk_name == 'sPLT':
            return ChunkParser.get_sPLT_data(chunk_data)
        elif chunk_name == 'sTER':
            return ChunkParser.get_sPLT_data(chunk_data)
        elif chunk_name == 'sRGB':
            return ChunkParser.get_sPLT_data(chunk_data)
        elif chunk_name == 'oFFs':
            return ChunkParser.get_sPLT_data(chunk_data)
        else:
            return None

    @staticmethod
    def save_chunk_data_to_json(chunk_name, chunk_data, output_folder):
        if chunk_name == 'IHDR':
            with open(output_folder + 'IHDR.json', 'w') as f:
                json.dump(ChunkParser.get_IHDR_data(chunk_data), f)
        elif chunk_name == 'tEXt':
            with open(output_folder + 'tEXt.json', 'w') as f:
                json.dump(ChunkParser.get_tEXt_data(chunk_data), f)
        elif chunk_name == 'tIME':
            with open(output_folder + 'tIME.json', 'w') as f:
                json.dump(ChunkParser.get_tIME_data(chunk_data), f)
        elif chunk_name == 'pHYs':
            with open(output_folder + 'pHYs.json', 'w') as f:
                json.dump(ChunkParser.get_pHYs_data(chunk_data), f)
        elif chunk_name == 'gAMA':
            with open(output_folder + 'gAMA.json', 'w') as f:
                json.dump(ChunkParser.get_gAMA_data(chunk_data), f)
        elif chunk_name == 'hIST':
            with open(output_folder + 'chunk_hIST.json', 'w') as f:
                json.dump(ChunkParser.get_hIST_data(chunk_data), f)
        elif chunk_name == 'iTXt':
            with open(output_folder + 'iTXt.json', 'w') as f:
                json.dump(ChunkParser.get_iTXt_data(chunk_data), f)
        elif chunk_name == 'sPLT':
            with open(output_folder + 'sPLT.json', 'w') as f:
                json.dump(ChunkParser.get_sPLT_data(chunk_data), f)
        elif chunk_name == 'sTER':
            with open(output_folder + 'sTER.json', 'w') as f:
                json.dump(ChunkParser.get_sPLT_data(chunk_data), f)
        elif chunk_name == 'sRGB':
            with open(output_folder + 'sRGB.json', 'w') as f:
                json.dump(ChunkParser.get_sPLT_data(chunk_data), f)
        elif chunk_name == 'oFFs':
            with open(output_folder + 'oFFs.json', 'w') as f:
                json.dump(ChunkParser.get_sPLT_data(chunk_data), f)
        else:
            return None