# ImageResizer
This app resizes all images specified in a folder.<br>


## Preriquisites
.net core 3.1 runtime installed on your machine<br>

## How to use
**Usage**:<br>
imageResizer [options]<br>

**Options**:<br>
  -b|--behaviour      shall the new images override the original? it defaults to copy.<br>
  -s|--source-path    source path that contains the images. it defaults to the path where the program is found.<br>
  -t|--target-path    target path the resized images will be saved to. it defaults to the path where the program is found.<br>
  -f|--scale-factor   a value bigger than 1 (percent value) that defines the scale percent. Example: -f 25 => scale factor will be 25% of the original image<br>
  
**examples**:<br>
    imageResizer.exe -b replace -f 20 -s D:/Dropbox/SellingItems          ## parses all images in the folder "SellingItems" and resize them down by 20%<br>
    imageResizer.exe -b replace -f 150 -s D:/Dropbox/SellingItems         ## parses all images in the folder "SellingItems" and resize them up by 150%<br>
    imageResizer.exe -b copy -f 50 -s D:/Dropbox/SellingItems -t D:/output          ## parses all images in the folder "SellingItems" and resize them down by 50%<br>