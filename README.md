# Image Viewer

## Introduction

This program is a simple image viewer. It can be used to view png images, draws histograms and shows chunks of the image. Programm can store data about images in a database.
Program is based on .NET MAUI and runs image analysis python code in the background. Works best on Windows.

## Installation

You need to have the VisualStudio environment and python interpreter installed on your computer. You can download VisualStudio from [here](https://visualstudio.microsoft.com/pl/downloads/). You can download python from [here](https://www.python.org/downloads/).

In order to run the program you need to change the main path in "Model/PythonScripts/ImageeProcess.py" in line 40 to the path where you have the "Image_Viewer_1.0/Resources" folder.

instalation:
```python

main_path = "C:\\Users\\marek\\OneDrive\\Dokumenty\\GitHub\\Image_Viewer\\Image_View_MVVC\\Image_View_V1.0\\Resources\\"

```


## Usage

Select the png image you want to display. On the main screen you will see the selected image and on the right: its name, thumbnail, histogram and the information it contains.
