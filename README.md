# Emojifier
A TShock emoji plugin.

## Configuration
When plugin runs for the first time it'll create a file named "EmojifierConfig.json":
```json
{
  "Emojis": {
    "skull": 193,
    "heart": 29
  }
}
```
Here, the keys are the _***keywords***_ that will be turned into items in chat. <br> 
And, the numbers are the _***item IDs***_ of the items that keywords will turn into. <br>
For example when player types "Hello there! :<zero-width space>heart:" in chat. It will turn into "Hello there! :heart:"
