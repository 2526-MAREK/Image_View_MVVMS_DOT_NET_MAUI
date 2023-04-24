import cv2
import numpy as np
import matplotlib.pyplot as plt


class ImageAnalysis:

    def __init__(self):
        pass

    @staticmethod
    def fft_of_image(image_path, output_path, draw_plots):

        img = cv2.imread(image_path, cv2.IMREAD_GRAYSCALE)
        # if image is 1x1 break
        if img.shape[0] == 1 and img.shape[1] == 1:
            return

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
    def histogram_of_image(image_path, output_folder, output_imgs_folder, draw_plots, hist_rgb, hIST):
        # Load the image
        img = cv2.imread(image_path)
        img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

        # Compute histograms
        # if hIST is None:
        hist_gray = cv2.calcHist([img_gray], [0], None, [256], [0, 256])
        # else:
        # hist_gray = hIST
        if hist_rgb:
            hist_r = cv2.calcHist([img_rgb], [0], None, [256], [0, 256])
            hist_g = cv2.calcHist([img_rgb], [1], None, [256], [0, 256])
            hist_b = cv2.calcHist([img_rgb], [2], None, [256], [0, 256])


        fig = plt.figure(figsize=(10, 4))
        ax = plt.axes()
        fig.set_facecolor((0.09375, 0.09375, 0.09375))
        ax.set_facecolor((0.2, 0.2, 0.2))

        frame1 = plt.gca()  # Remove the labels:
        frame1.axes.xaxis.set_ticklabels([])
        frame1.axes.yaxis.set_ticklabels([])

        plt.plot(hist_gray, color='w')
        if hist_rgb:
            plt.plot(hist_r, color='r')
            plt.plot(hist_g, color='g')
            plt.plot(hist_b, color='b')
        plt.tight_layout()
        plt.savefig(output_imgs_folder + 'hist.png')
        if draw_plots:
            plt.show()
