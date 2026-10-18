+++
title = "Implementing Machine Learning in Unity"
date = "2026-10-18"
draft = "false"
tags = ['Unity', 'ML']
+++

## Implementing Machine Learning in Unity

In this post, we will learn how to implement a Machine Learning Agent in Unity. In order to do that, we will install the Unity ML-Agents Toolkit, that allows to train an agent to complete tasks. We will first follow the installation guide and check one of the built-in examples, and then create our own agent from scratch.

### Setting up the ML-Agents Toolkit

This part basicaly follows the guide of the [official Unity ML-Agents Toolkit manual](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.0/manual/Installation.html). If any problem occurs, please refer to this page.


#### Prerequisites

Before installing the toolkit, ensure you have the correct foundation:

- Unity Editor: You must be using Unity version 6000.0 or later. Use Unity Hub to manage your installation.

- Conda (or Mamba): You need a virtual environment manager to keep the ML-Agents Python dependencies isolated from the rest of your system.

#### 1. Creating the Python Environment

Python 3.10.12 is prefered. In a terminal, use Conda to create a new virtual environment specifically running Python 3.10.12, then activate it:

```
conda create -n mlagents python=3.10.12 && conda activate mlagents
```

If you are on Windows, you will need to install PyTorch separately at this stage to ensure proper CUDA support for your GPU).

```
pip3 install torch~=2.2.1 --index-url https://download.pytorch.org/whl/cu121
```

#### 2. Installing the ML-Agents Unity Package

Next, we need to add the toolkit to your Unity project. This is done via the Unity Editor:

1. Open your project in Unity.

2. Go to Window > Package Manager.

3. Click the + icon and select Add package by name.

4. Type <code>com.unity.ml-agents </code> and install it.

#### 3. Installing the ML-Agents Python Package
Finally, you need to install the Python side of the toolkit so your algorithms can communicate with Unity. Run this inside your Conda environment:

```
python -m pip install mlagents==1.1.0
```


### The 3D Ball example

{{< image src="featured_UnityML.png" alt="UnityML" position="center" style="border-radius: 4px;" >}}
<em> The 3DBall example in training </em>

One of the examples Unity provides when installing ML-Agents is the 3D Ball environment, which contains several agent cubes and balls. In this environment, an agent cube is an Agent that receives a reward for every step that it balances the ball. An agent is also penalized with a negative reward for dropping the ball. The goal of the training process is to have the agents learn to balance the ball on their head. Now, we will quickly follow the steps from [this page](https://docs.unity3d.com/Packages/com.unity.ml-agents@4.0/manual/Sample.html).

First, make sure you open the ML-Agents project that was setup earlier. In the Project window, you should now have access to <code> Assets/ML-Agents/Examples/3DBall/Scenes</code>, where you find the 3DBallscene file. Open it.

#### Run a pre-trained model
Let's run a model that Unity has already trained for us.

1. In your Hierarchy, you will see several platforms (they are all identical clones to speed up training). Select the main 3DBall prefab so we can edit them all at once.
2. In the Inspector window, look for a component called Behavior Parameters. This script dictates how the agent makes decisions.
3. Go to your Project window and navigate to <code>Assets/ML-Agents/Examples/3DBall/TFModels </code>.
4. You will see a file named 3DBall (this is the pre-trained <code>.onnx </code> neural network model). Drag and drop this file into the Model slot on the Behavior Parameters component.
5. Make sure the Inference Device is set to CPU.
6. Press Play at the top of the Unity Editor and see the model in action !


#### Train a new model from scratch
Now let's try to train our own 3DBall model.

1. Make sure you are using the Conda environment we created in the setup guide :
```
conda activate mlagents
```
2. Navigate to the root directory where you cloned the ml-agents repository. Run the following command to start the training process:
```
mlagents-learn config/ppo/3DBall.yaml --run-id=first3DBallRun
```
3. The terminal will initialize and eventually display a message asking you to press the Play button in the Unity Editor. Go back to the Unity Editor and press Play. You can observe the training in the Unity Editor and the terminal, where you can see the results improving as the step go further.

{{< image src="UnityMLtraining.png" alt="Unity ML Training" position="center" style="border-radius: 4px;" >}}
<em> Screenshot of the terminal during training </em>

Once the training is complete, Unity automatically generates a <code> .onnx
</code> neural network file. You can find it in the <code> /results </code> folder within the ML-Agents directory.


### Making our own environment
For our own environment, we will train a ball agent to go towards target objects that are generated randomly inside the training area.

First let's create the scene. In a new project (or a new Scene in the ML-Agents project), add a Plane for the ground, Walls if you want boundaries, a Sphere object that will be our agent, and a target object (a red square in my case). Your Scene should look like the following :

{{< image src="MLscene.png" alt="ML Scene" position="center" style="border-radius: 4px;" >}}

Add a RigidBody to the agent so it can interacts with physics, and create a tag 'floor' for the floor plane.

Now let's implement the Agent Script to our agent object. Create a new "RollerAgent" script in the agent object. ML Agents splits training in episodes, where the Agents (in ou rcase the sphere) tries to solve the task. The file shoulf look like the following :

{{< code file="assets/code/RollerAgent.cs" >}}

In this scipt, we extend three methods from the Agent base class : OnEpisodeBegin, CollectObservations and OnActionReceived. The Heuristic method is also included for keyboard testing.

Now select the Agent and drag the Target object into the public "Target" field of the script. Add a 'Decision Requester' component with a decision period of 10. Add a 'Behavior Parameters' component and configure it to match the script : BehaviorName = RollerBall (the name of your agent), Vector Observation > Space Size = 8, Actions > Continuous Actions = 2.

{{< image src="MLsettings.png" alt="ML Settings" position="center" style="border-radius: 4px;" >}}
<em> Screenshot of how the settings of the ML Agent should look like</em>

To prepare for training, create a configuration file in the <code> config/ppo </code> (let's name ours <code> rollerball_config.yaml </code>). This file contains hyperparameters like batch size, buffer size, and learning rate. Here is the file I used : 

{{< code file="assets/code/rollerball_config.yaml" lang="yaml" >}}

Now we can start the training. From the working directory, use this command :
```
mlagents-learn config/rollerball_config.yaml --run-id=RollerBall
```

Press the PLay button in the Unity Editor to watch the training process. Here are a video from the beginning of the training, where we see the agent struggling to reach the target, and an other from the end where it became very efficient and reaches multiple target per episodes.

{{< localvideo src="/videos/debutTraining.mp4" >}}
<em> Beginning of the training</em>

{{< localvideo src="/videos/finTraining.mp4" >}}
<em> End of the training</em>



