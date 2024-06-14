import cv2
import numpy as np

def encode_video(payload, cover_object, num_bits):
    # Convert payload text to binary
    binary_payload = ''.join(format(ord(c), '08b') for c in payload)

    # Append termination bits to the payload
    binary_payload += '00' * num_bits * 4

    # Open the cover object video using OpenCV
    cover_video = cv2.VideoCapture(cover_object)

    # Get video properties
    frame_count = int(cover_video.get(cv2.CAP_PROP_FRAME_COUNT))

    # Iterate through each frame
    for i in range(frame_count):
        ret, frame = cover_video.read()
        if not ret:
            break

        # Convert the frame to a NumPy array
        frame_array = np.array(frame)

        # Iterate through each pixel of the frame
        for row in frame_array:
            for pixel in row:
                # Convert pixel values to binary
                binary_pixel = ''.join(format(value, '08b') for value in pixel)

                # Retrieve the payload bits to be encoded
                payload_bits = binary_payload[:num_bits]

                # Modify the LSBs of the pixel values based on the payload
                modified_pixel = binary_pixel[:-num_bits] + payload_bits
                binary_payload = binary_payload[num_bits:]

                # Convert the modified pixel back to integer
                new_pixel = [int(modified_pixel[i:i+8], 2) for i in range(0, len(modified_pixel), 8)]

                # Update the pixel value in the frame
                pixel[:] = new_pixel

                if len(binary_payload) == 0:
                    break
            if len(binary_payload) == 0:
                break
        if len(binary_payload) == 0:
            break

    # Save the modified frames as a new video
    # replace with file path for where the encoded video is to be saved
    output_file = 'C:/Users/JW/OneDrive - Singapore Institute Of Technology/SIT/Year 1/Trimester 3/INF2005 Cyber Security Fundamentals/Week 4/ACW1/acw1/mp4/encoded_video.mp4'
    output_video = cv2.VideoWriter(output_file, cv2.VideoWriter_fourcc(*'mp4v'), 30, (frame.shape[1], frame.shape[0]))
    for i in range(frame_count):
        ret, frame = cover_video.read()
        if not ret:
            break
        output_video.write(frame)
    output_video.release()
    cover_video.release()

    print("Encoding completed. Encoded video saved as", output_file)

def decode_video(encoded_video, num_bits):
    # Open the encoded video using OpenCV
    encoded_video = cv2.VideoCapture(encoded_video)

    # Get video properties
    frame_count = int(encoded_video.get(cv2.CAP_PROP_FRAME_COUNT))
    print("Frame count:", frame_count)

    # Initialize the decoded payload and the number of termination bits encountered
    decoded_payload = ""
    termination_bits_count = 0

    # Iterate through each frame
    for i in range(frame_count):
        print("Processing frame", i + 1)

        ret, frame = encoded_video.read()
        if not ret:
            break

        # Convert the frame to a NumPy array
        frame_array = np.array(frame)

        # Iterate through each pixel of the frame
        for row in frame_array:
            for pixel in row:
                # Convert pixel values to binary
                binary_pixel = ''.join(format(value, '08b') for value in pixel)
                print("Binary pixel:", binary_pixel)

                # Extract the LSBs of the pixel values
                extracted_bits = binary_pixel[-num_bits:]
                print("Extracted bits:", extracted_bits)

                # Accumulate the extracted bits to reconstruct the payload
                decoded_payload += extracted_bits

                # Check if the termination bits are encountered
                if decoded_payload[-num_bits:] == '00':
                    termination_bits_count += 1
                else:
                    termination_bits_count = 0

                # Check if the termination bits have been encountered consecutively
                if termination_bits_count >= num_bits * 4:
                    # Remove the termination bits from the payload
                    decoded_payload = decoded_payload[:-(num_bits * 4)]
                    break

            if termination_bits_count >= num_bits * 4:
                break

        if termination_bits_count >= num_bits * 4:
            break

    encoded_video.release()

    if termination_bits_count >= num_bits * 4:
        # Convert the binary payload to text
        decoded_text = ''.join(chr(int(decoded_payload[i:i + 8], 2)) for i in range(0, len(decoded_payload), 8))
        print("Decoding completed. Decoded payload:", decoded_text)
        return decoded_text
    else:
        print("Decoding failed. Payload termination bits not found.")
        return None

# Test
payload = 'Hello, this is a secret message!'
# replace with file path for video.mp4 (.../mp4/video.mp4)
cover_video = 'C:/Users/JW/OneDrive - Singapore Institute Of Technology/SIT/Year 1/Trimester 3/INF2005 Cyber Security Fundamentals/Week 4/ACW1/acw1/mp4/video.mp4'
num_bits = 2

# Encode the payload into the cover video
encode_video(payload, cover_video, num_bits)

# Decode the payload from the encoded video
# replace with file path for the encoded video (.../encoded_video.mp4)
encoded_video = 'C:/Users/JW/OneDrive - Singapore Institute Of Technology/SIT/Year 1/Trimester 3/INF2005 Cyber Security Fundamentals/Week 4/ACW1/acw1/mp4/encoded_video.mp4'  # Provide the path to the generated encoded video
decode_video(encoded_video, num_bits)