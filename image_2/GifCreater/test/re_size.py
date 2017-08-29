import images2gif
from PIL import Image
import os
import imageio

if os.path.exists("GIF"):
    for file in os.listdir("GIF/"):
        if len(file.partition(".")[1]) != 0:
            os.remove("GIF/"+file)
        else:
            os.rmdir("GIF/"+file)
    os.rmdir("GIF")

l = os.listdir(".")
res = l.copy()
for s in l:
    if len(s.partition(".gif")[1]) == 0:
        res.remove(s)
l = res.copy()
os.mkdir("GIF")
i=0;
n=len(l)
for dir in l:
    #l2 = os.listdir(dir+"/")
    #frames = [imageio.imread(dir+"/"+im) for im in l2]
    #frames = imageio.mimread(dir)
    reader = imageio.get_reader(dir)
    for i, im in enumerate(reader):
        imageio.imwrite('GIF/test.png',im[:,:,0])
        break
    break
    #print(dir)


