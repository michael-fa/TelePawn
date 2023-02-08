//TeleAMX (C) 2023 Michael Fanter - fanter.eu
//This adds the complete functionality for telegram pawn scripts.

//Core
native Loadscript(script[]);
native Unloadscript(script[]);
native CallRemoteFunction(function[], format[], {Float,_}:...);

#define CHAT_TYPE_PRIVATE           0
#define CHAT_TYPE_GROUP             1
#define CHAT_TYPE_CHANNEL           2
#define CHAT_TYPE_SENDER            3
#define CHAT_TYPE_SUPERGROUP        4

//Telegram functions

//Chats
native GetChatType(chatid[]);
native GetChatSlowModeDelay(chatid[]);
native GetChatDescription(chatid[], dest_str[]);
native UnpinAllChatMessages(chatid[]);

//Message
native SendChatMessage(chatid[], message[], reply_messageid = -1);
native DeleteChatMessage(chatid[], messageid);
native PinChatMessage(chatid[], messageid);
native UnpinChatMessage(chatid[], messageid);
native EditChatMessage(chatid[], messageid, new_text[]);

//User
native GetUserName(chatid[], userid[], dest_string[]);
native GetUserLegalName(chatid[], userid[], dest_string[]);
native GetUserBio(chatid[], dest_string[]); //May not work if the bot never had end to end chat with the user before.

//Bot itself
native GetBotUsername(dest_string[]);
native GetBotName(dest_string[]);

forward OnFilterscriptInit();
forward OnMessage(chatid[], messageid, senderid[], text[]);
forward OnMessageEdited(chatid[], messageid, senderid[], new_text[]);
forward OnTelegramError(errorcode, error_source[], error_message[], error[]);
