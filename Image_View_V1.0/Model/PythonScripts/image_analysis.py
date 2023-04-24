import cv2
import numpy as np
import matplotlib.pyplot as plt


class ImageAnalysis:

    def __init__(self):
        pass

    @staticmethod
    def fft_of_image(image_path, output_path, draw_plots):

        img = cv2.imread(image_path, cv2.IMREAD_GRAYSCALE)
        f = np.fft.fft2(img)
        f_shift = np.fft.fftshift(f)
        magnitude_spectrum = 20 * np.log(np.abs(f_shift))

        if draw_plots:  # Display the input image and magnitude spectrum
            plt.subplot(121), plt.imshow(img, cmap='gray')
            plt.title('Input Image'), plt.xticks([]), plt.yticks([])
            plt.subplot(122), plt.imshow(magnitude_spectrum, cmap='gray')
            plt.title('Magnitude Spectrum'), plt.xticks([]), plt.yticks([])
            plt.show()

        cv2.imwrite(output_path + 'fft.png', magnitude_spectrum)  # Save the magnitude spectrum to a file

    @staticmethod
    def histogram_of_image(image_path, output_folder, output_imgs_folder, draw_plots):
        # Load the image
        img = cv2.imread(image_path)
        img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

        # Compute histograms
        hist_gray = cv2.calcHist([img_gray], [0], None, [256], [0, 256])
        hist_r = cv2.calcHist([img_rgb], [0], None, [256], [0, 256])
        hist_g = cv2.calcHist([img_rgb], [1], None, [256], [0, 256])
        hist_b = cv2.calcHist([img_rgb], [2], None, [256], [0, 256])

        fig = plt.figure()
        fig.set_facecolor((0.09375, 0.09375, 0.09375))
        ax = plt.axes()
        ax.set_facecolor((0.2, 0.2, 0.2))
        plt.plot(hist_gray, color='w')
        plt.plot(hist_r, color='r')
        plt.plot(hist_g, color='g')
        plt.plot(hist_b, color='b')
        plt.tight_layout()
        plt.savefig(output_imgs_folder + 'hist.png')
        if draw_plots:
            plt.show()
