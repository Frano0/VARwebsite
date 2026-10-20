+++
title = "Ideating & Prototyping an AR app"
date = "2026-10-19"
draft = "false"
tags = ['Human-Computer Interface']
+++


In this post, I will explain how I sketched my own AR application idea, made a first prototype and evaluated it.

## Sketching an idea

As a DJ, I find interest in searching through new music, and find importance in finding the perfect song to play next. Nowadays, most DJs have their music on digital format using USB flashdrives. But the true old-school way is defferent : they use vinyls. I always wanted to try learning vinyl DJing, but there are a lot of new things to learn. One of these is knowing all the information you need to select the song. On digital formats, it easy as all the information is stored in the metadata, and accessible on the DJing device. On the contrary on vinyls, the information is harder to find, often never in the same place and sometimes totally absent. My app idea would help index all this informations, while also recognising vinyls when the user picks them up, displaying the relevant informations. This would make selecting the next song similar to digital devices, without obstructing the flow of the user.

---

## Making a prototype

I decided to make my prototype by imagining how the product would look when the user will use it. In the case of my idea, the app would be used when the user is searching through vinyls. When searching, the app would recognize a vinyl and an icon would hover over the disk for the user to choose if they want to see more informations. For the integration, I had several ideas: would it be better to have a smaller, less heavy extend icon so the user isn't to visually burdened by the app when they doesn't want to have all the information in front of their eyes; or something more obvious ? When showing the informations, would it be better to have the window to float above each vinyl, or have all the windows aligned so that the user could better compare his selection ?
Here are how my different proposition look:

{{< image src="proto_vinyl_shrunk1.png" alt="AR app prototype 1" position="center" style="border-radius: 4px;" >}}
{{< image src="proto_vinyl_shrunk2.png" alt="AR app prototype 2" position="center" style="border-radius: 4px;" >}}
<em>Propositions for the "shrunk" window</em>

{{< image src="proto_vinyls.png" alt="AR app prototype 3" position="center" style="border-radius: 4px;" >}}
{{< image src="proto_vinyl_2.png" alt="AR app prototype 4" position="center" style="border-radius: 4px;" >}}
<em>Propositions for the full window</em>

---

## Evaluating the prototype

I showed my prototype to a friend (that DJs too !) to get his feedback:

- **Window anchoring** : better to follow the vinyl, as it is more natural for the user, and they can do the comparison by placing both vinyl side by side
- **Icon choice** : better to keep it simple (no text). But how to actually use it if both hands are not free ? Maybe a quick tilt of the vinyl to indicate wanting to expand/shrink the window
- **Displayed information** : checkmarks are unclear. For example for the BPM, it would be better to indicate a percentage offset from the other vinyl, or in the case of someone DJing, the difference with the song playing (ex: <em>+3.5% from Master</em>)


Those feedbacks not only helped choice among the ideas I already had, but also made new propositions emerge from the perspective of an user actually using the solution !
