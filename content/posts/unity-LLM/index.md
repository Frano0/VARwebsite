+++
title = "Implementing a LLM Chatbot in Unity"
date = "2026-10-18"
draft = "false"
tags = ['Unity', 'LLM']
+++

## Building a Talking LLM Character in Unity

In this tutorial, we will look at how to build an in-engine chat interface where an NPC responds **locally on your machine** using [LLMUnity](https://github.com/undreamai/LLMUnity).


### Architecture Overview

The system architecture consists of four distinct components working together:

1. **`LLM_GameObject` (The Brain):** A GameObject with the `LLM` script that loads the local `.gguf` weights and runs inference in the background.
2. **`LLMAgent` (The Character):** The client representation holding the agent's character persona and system prompt. Multiple agents can connect to a single `LLM` backend.
3. **`NPCController.cs` (The Glue):** A custom C# script handling UI events, locking input while the character speaks, and updating the text stream.
4. **`ChatScrollView` (The Face / UI):** A responsive TextMeshPro Scroll View that dynamically displays the conversation and scrolls automatically.

### Step 1: Install LLMUnity & Setup the Model Server

{{< image src="LLMgit.png" alt="Screenshot from the LLM for Unity Github" position="center" style="border-radius: 4px;" >}}

1. Open your Unity project ('6000.0.x' or later.
2. Navigate to **Window > Package Manager**.
3. Click the **`+`** button in the upper-left corner and select **Add package from git URL...**.
4. Enter `https://github.com/undreamai/LLMUnity.git` and click **Add**.
5. Once imported, right-click in your Hierarchy and choose **Create Empty**. Name this object `LLM_GameObject`.
6. With `LLM_GameObject` selected, go to the Inspector, click **Add Component**, and search for **LLM**.
7. In the LLM component inspector:
   * Keep default settings: `Num Threads = -1` (uses all CPU cores) and `Context Size = 8192`.
   * Under the model list, select and download a lightweight model like **Qwen 3.5 0.8B** (fast initial token latency, concise chat answers)
   * Wait for the model download to complete.
   


### Step 2: Create the AI Character

1. Right-click in the Hierarchy and create another Empty GameObject named `AI Character`.
2. Add the **LLMAgent** component to it.
3. In the Inspector, ensure the **Llm** property references the `LLM_GameObject` created in Step 1.
4. Customize the **System Prompt** to define your character's personality and interaction boundaries:

```text
You are an AI conversational agent.
Respond naturally and briefly (1-3 sentences), as if having a real-time spoken conversation with a user testing your interface.
Do not use emojis.
```

### Step 3: Build the Chat UI

The UI Canvas hierachy should look something like this :
```
Canvas
├── ChatScrollView (Scroll View)
│   └── Viewport
│       └── Content (Vertical Layout Group + Content Size Fitter)
│           └── ChatLogText (TextMeshPro - Text)
├── InputField_UserMessage (TMP Input Field)
└── Button_Send (TMP Button)
```

Some settings have to be changed as well, so the generated text fits the window perfectly:

- Set UI Scale Mode to Scale With Screen Size and Reference Resolution to 1920 x 1080.
- ChatScrollView :
	- Width: 700, Height: 300, Pos Y: 90.
	- isable horizontal scrolling on the Scroll Rect component (Horizontal = unchecked).
- Content:
	+ Add a Vertical Layout Group (Control Child Size Width: Checked).
	+ Add a Content Size Fitter and set Vertical Fit to Preferred Size.
- ChatLogText:
	+ Enable Word Wrapping.
	+ Add a Content Size Fitter with Vertical Fit set to Preferred Size.
	+ Font Size: 24 or to liking.
	
Here is how it looked for me :

{{< image src="LLMscreen.png" alt="LLM UI Screenshot" position="center" style="border-radius: 4px;" >}}


### Step 4: Create the Controller Script

Create a new C# script named <code>NPCController.cs</code> in your Assets/Scripts/ folder and paste the following code:

{{< code file="assets/code/NPCController.cs" >}}

### Step 5: Assign Component References
1. Select the AI Character GameObject
2. Add the NPCController component to it.
3. Link the inferences in the Inspector:
	- Llm Agent: Drag the AI Character object (holds the LLMAgent component).
	- Input Field: Drag InputField_UserMessage.
	- Chat Log Text: Drag ChatLogText.
	- Send Button: Drag Button_Send.
	- Scroll Rect: Drag ChatScrollView.

### Step 6: Try it out !
Press Play in the Unity Editor, click into the input field, and send a message.
Here is a video demonstrating the final results.

{{< localvideo src="LLMUnity.mp4" >}}
<em> The LLM in action </em>

Now you can play around with the different parameters available, for example the **System Prompt** to see how it impact the answer from the LLM Agent.


