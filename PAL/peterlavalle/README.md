
This is my sprawling pile of stuff for Unity ...


# Checkout (for an existing project)

```
$ git clone git@gitlab.com:g-pechorin/unity-peter-lavalle.git ./-hugit
$ mv ./-hugit/.git .hugit
$ rm -dr ./-hugit/
$ echo "# Ignore all ..." >> .hugit/.gitignore
$ echo "*" >> .hugit/.gitignore
$ echo "# ... except us" >> .hugit/.gitignore
$ echo "\!.gitignore" >> .hugit/.gitignore
```

# Install (into a new project)

```
$ git clone git@gitlab.com:g-pechorin/unity-peter-lavalle.git ./peterlavalle
$ mv ./peterlavalle/.git ./peterlavalle/.hugit
$ echo "# Ignore all ..." >> ./peterlavalle.hugit/.gitignore
$ echo "*" >> ./peterlavalle.hugit/.gitignore
$ echo "# ... except us" >> ./peterlavalle.hugit/.gitignore
$ echo "\!.gitignore" >> ./peterlavalle.hugit/.gitignore
```
