# xkcd-wallpaper
This program is a .NET application that reads a random comic from xkcd.com and sets it as the desktop wallpaper.
Features:
  1) Windows Tray Icon
    a) Left click gets a new random comic
    b) Right click shows context menu with the following options
      Next Comic
      Open in Browser
      Exit
  2) Will not choose a comic that will not fit in primary monitor working area
  3) Gets a new comic every 15 minutes
  4) Responds to PowerModeChanged SystemEvent to pause the timer
  5) Post-build event copies .exe to Startup folder
  
  Desired Future State:
  1) Modify bitmap to include the tooltip text for the comic

Let me know what you think!
