I tried to keep this as simple as possible, while still having some modern features like localization of messages.

The resource designer is a nice feature, we don't have that in java/eclipse.

I was planning to do some Unit Testing, but I'm not sure I have time for that.

Design Decision:

1) I'm serializing the user data (user name, 3 questions id's and 3 responses) as json. I assigned the question id's based on the sequence they are loaded into the string array. This will be an issue if the questions change sequence in the array and someone changes the text of the questions as it will no longer match exactly with what the users response was. 