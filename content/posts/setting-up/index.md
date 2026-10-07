+++
title = "Setting Up a Hugo Website with the Blowfish Theme"
date = "2026-09-18"
draft = "false"
+++

## Setting Up a Hugo Website with the Blowfish Theme

Hugo is a simple and easy-to-use tool for building static website. This website was built using Hugo, implementing the **Blowfish** theme. It is hosted with GitHub Pages

Here, I’ll walk you through setting up a brand-new Hugo project from scratch, manually installing the Blowfish theme, adding your first piece of content, building and hosting the final site.

This walk-through is basically following the [Hugo quickstart guide](https://gohugo.io/getting-started/quick-start/), the [Blowfish installation guide](https://blowfish.page/docs/installation/) and the [Blowfish hosting guide](https://blowfish.page/docs/hosting-deployment/).

### Prerequisites

Before we dive in, make sure you have the following ready to go:

* **Hugo installed** (v0.158.0 or later).

* **Git installed**.

* A terminal (I made this website on Linux).*

### Step 1: Create Your Project Skeleton

First, open your terminal and run the following commands to generate your project folder and initialize it:

```
# Create a new project called 'my-website'
hugo new project my-website

# Move into your new project directory
cd my-website

# Initialize an empty Git repository
git init

```

### Step 2: Install the Blowfish Theme Manually

There are many ways to install Blowfish. I decided to do it manually

1. Head over to the Blowfish GitHub repository and download the latest release of the theme's source code.

2. Extract the downloaded archive.

3. Rename the extracted folder to exactly `blowfish`.

4. Move this folder into the `themes/` directory inside your Hugo project's root folder.

### Step 3: Configure the Theme

Blowfish uses a structured configuration folder rather than a single file. Here is how to set it up:

1. In the root folder of your website, **delete** the default `hugo.toml` file that Hugo automatically generated for you.

2. Navigate to `themes/blowfish/config/_default/` and copy all the `.toml` files you see there.

3. Paste those files into your project's own `config/_default/` folder (create these folders in your root directory if they don't exist).

Once you copy the files over, your project's config folder should look like this:

```
config/_default/
├─ hugo.toml
├─ languages.en.toml
├─ markup.toml
├─ menus.en.toml
└─ params.toml

```

**Crucial Step:** Because we installed the theme manually (without using Hugo Modules), you need to open your new `config/_default/hugo.toml` file and add this exact line to the very top:

```
theme = "blowfish"

```

While you have `hugo.toml` open, it's also a good time to personalize your site's core settings:

```
baseURL = 'https://yourwebsite.com/'
locale = 'en-us'
title = 'My Awesome Blowfish Site'

```

### Step 4: Create Your First Post

In the particular case of making a blog, creating posts is easy thanks to Hugo. You can do it by using the following command:  

```
hugo new content content/posts/my-first-post.md

```

Hugo will create the file and automatically populate it with some "front matter" (the metadata at the top). Open it in your favorite text editor, and it will look something like this:

```
+++
title = 'My First Post'
date = 2024-01-14T07:07:07+01:00
draft = true
+++

## Introduction
This is **bold** text, and this is *emphasized* text. Welcome to my new blog!

```


### Step 5: Preview Your Website

To see what your website looks like as you edit, start Hugo's local development server. Because our post is still a draft, we need to append the `-D` flag to tell Hugo to render draft content:

```
hugo server -D

```

Open the localhost URL provided in your terminal in your web browser. You can keep this server running in the background—it will automatically refresh the page whenever you save changes to your files!

### Step 6: Host the Website on GitHub Pages

This website is also hosted on GitHub Pages. Let's see how to do this.

First, make sure your Hugo project is pushed to a repository on GitHub. Once it's there, we need to create an Actions workflow to automatically build and deploy your site every time you make a change.

Create a new file in your project located at .github/workflows/gh-pages.yml and paste the following configuration into it:

```
# .github/workflows/gh-pages.yml
name: GitHub Pages
on:
  push:
    branches:
      - main
jobs:
  build-deploy:
    runs-on: ubuntu-24.04
    concurrency:
      group: ${{ github.workflow }}-${{ github.ref }}
    steps:
      - name: Checkout
        uses: actions/checkout@v3
        with:
          submodules: true
          fetch-depth: 0
      - name: Setup Hugo
        uses: peaceiris/actions-hugo@v2
        with:
          hugo-version: "latest"
      - name: Build
        run: hugo --minify
      - name: Deploy
        uses: peaceiris/actions-gh-pages@v3
        if: ${{ github.ref == 'refs/heads/main' }}
        with:
          github_token: ${{ secrets.GITHUB_TOKEN }}
          publish_branch: gh-pages
          publish_dir: ./public

```


Ensure that the branch name (e.g., main) matches your actual source branch in both the branches: section and the deploy step's if: parameter.

Head over to Settings > Pages in your GitHub repository and change the source to GitHub Actions. This will tell the repository to deploy the website to your *username.github.io* domain.

Save the gh-pages.yml file and push this new configuration up to your GitHub repository. The GitHub Action should trigger automatically.


**Everything should be ready to go !**