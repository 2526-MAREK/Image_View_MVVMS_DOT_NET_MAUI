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
    def transform_hist(hist_data):
        hist = hist_data['Histogram']
        result = np.zeros((256, 1), dtype=float)

        for i in range(len(hist)):
            result[i] = float(hist[i])

        return result


    @staticmethod
    def histogram_of_image(image_path, output_folder, output_imgs_folder, draw_plots, hIST):
        # Load the image
        img = cv2.imread(image_path)
        img_gray = cv2.cvtColor(img, cv2.COLOR_BGR2GRAY)
        img_rgb = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

        # Plor parameters
        x_vals = np.arange(0, 256, 1)
        bar_alpha = 0.5
        bar_width = 1
        fig_color = (0.09375, 0.09375, 0.09375)
        ax_color = (0.2, 0.2, 0.2)

        # Greyscale plot setup
        fig1 = plt.figure(figsize=(10, 4))
        ax1 = plt.axes()
        ax1.set_xlim([0, 256])
        fig1.set_facecolor(fig_color)
        ax1.set_facecolor(ax_color)
        frame = plt.gca()  # Remove the labels:
        frame.axes.xaxis.set_ticklabels([])
        frame.axes.yaxis.set_ticklabels([])

        if hIST is None:    # If no histogram data is given, calculate it
            hist_gray = cv2.calcHist([img_gray], [0], None, [256], [0, 256])
        else:
            hist_gray = ImageAnalysis.transform_hist(hIST)

        ax1.set_ylim([0, max(hist_gray) * 1.1])
        plt.bar(x_vals, hist_gray[:, 0], width=bar_width, color='w', alpha=bar_alpha)

        plt.tight_layout()
        plt.savefig(output_imgs_folder + 'hist.png')
        if draw_plots:
            plt.show()


        # RGB plot setup
        fig2 = plt.figure(figsize=(10, 4))
        ax2 = plt.axes()
        ax2.set_xlim([0, 256])
        fig2.set_facecolor(fig_color)
        ax2.set_facecolor(ax_color)
        frame = plt.gca()  # Remove the labels:
        frame.axes.xaxis.set_ticklabels([])
        frame.axes.yaxis.set_ticklabels([])

        hist_r = cv2.calcHist([img_rgb], [0], None, [256], [0, 256])
        hist_g = cv2.calcHist([img_rgb], [1], None, [256], [0, 256])
        hist_b = cv2.calcHist([img_rgb], [2], None, [256], [0, 256])

        ax2.set_ylim([0, max(max(hist_r), max(hist_g), max(hist_b)) * 1.1])
        plt.bar(x_vals, hist_r[:, 0], width=bar_width, color='r', alpha=bar_alpha)
        plt.bar(x_vals, hist_g[:, 0], width=bar_width, color='g', alpha=bar_alpha)
        plt.bar(x_vals, hist_b[:, 0], width=bar_width, color='b', alpha=bar_alpha)

        plt.tight_layout()
        plt.savefig(output_imgs_folder + 'hist_rgb.png')
        if draw_plots:
            plt.show()