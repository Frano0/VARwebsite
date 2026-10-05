+++
title = "Gaussian Splatting"
draft = "false"
date = "2026-10-02"
tags = ["Human-Computer Interface", "3D"]
+++


## Gaussian Splatting

Gaussian splatting is a 3D digital reconstruction and rendering technique that uses millions of tiny, soft, overlapping 3D shapes called Gaussians to recreate real-world scenes with photorealistic detail and real-time viewing speeds.
Here we use a database of photos to create make this reconstruction. We use Colmap, Brush and Supersplat to get the final result.

### Colmap

First, we load the image database into Colmap, to detect features on each of them, and reconstruct them together to get the relations between each images

{{< image src="colmap.png" alt="Colmap" position="center" style="border-radius: 4px;" >}}
<em> First visualisation of the object after detecting features and reconstructing </em>


### Brush

Now, we use brush in order to train the database and perform the reconstruction, using Machine Learning. The app allows the user to see the rendering progress in real time.

{{< image src="brush_wip.png" alt="BrushWIP" position="center" style="border-radius: 4px;" >}}
<em> Brush, training the database </em>

After the training, we can see that there are a lot of artifacts created, which we will try to remove to get a more polished look.

{{< image src="rendu_brush.png" alt="Rendu Brush" position="center" style="border-radius: 4px;" >}}
<em> Final result from Brush </em>

### Supersplat

To get our finished product, we use Supersplat to visualise the 3D object, as weell as remove any Gaussians that are not relevant to our 

{{< image src="rendu_cleaned.png" alt="Final product" position="center" style="border-radius: 4px;" >}}
<em> Final result after removing the irrelevant gaussians on Supersplat</em>

Here is a interactive representation of the 3D object :

{{< splat src="/gs_cleaned.splat" alt="Final result" height="500px" >}}






