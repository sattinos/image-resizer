# ImageResizer
This app resizes all images specified in a folder.<br>
It passes all images even those in subfolders.<br>

## Preriquisites
.net core 3.1 runtime installed on your machine<br>
The code is cross-platform, and can work on Windows, Mac or Linux.

## How to use
This is a console application. So, you can run it from the terminal.<br><br>
**Usage**:<br>
imageResizer [options]<br>

**Options**:<br>
  + -b|--behaviour<br>shall the new images override the original? it defaults to copy.<br><br>
  + -s|--source-path<br>source path that contains the images. it defaults to the path where the program is found.<br><br>
  + -t|--target-path<br>target path the resized images will be saved to. it defaults to the path where the program is found.<br><br>
  + -f|--scale-factor<br>a value bigger than 1 (percent value) that defines the scale percent.<br>for example: -f 25 => scale factor will be 25% of the original image<br>
  
**Examples**:<br>
    `imageResizer.exe -b replace -f 20 -s D:/Dropbox/SellingItems`<br>
    (parses all images in the folder "SellingItems" and resize them down by 20%)<br><br>
    `imageResizer.exe -b replace -f 150 -s D:/Dropbox/SellingItems`<br>
    (parses all images in the folder "SellingItems" and resize them up by 150%)<br><br>
    `imageResizer.exe -b copy -f 50 -s D:/Dropbox/SellingItems -t D:/output`<br>
    (parses all images in the folder "SellingItems" and resize them down by 50%)<br>