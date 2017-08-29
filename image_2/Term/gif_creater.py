from images2gif import writeGif
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
    if len(s.partition(".")[1]) != 0:
        res.remove(s)
l = res.copy()
os.mkdir("GIF")
i=0;
n=len(l)
for dir in l:
    l2 = os.listdir(dir+"/")
    frames = [imageio.imread(dir+"/"+im) for im in l2]
    name_of_file = "GIF/"+dir+".gif"#
    k = {'duration':1.0/10}
    print(name_of_file+"("+str(i)+"/"+str(n)+")")
    i=i+1
    imageio.mimsave(name_of_file,frames,'GIF',**k)


