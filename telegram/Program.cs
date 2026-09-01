using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Payments;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot.Exceptions;
var botClient = new TelegramBotClient("YOUR_TELEGRAM_TOKEN");
botClient.StartReceiving(HandleUpdateAsync, HandleErrorAsync);
Console.ReadLine();
async Task HandleUpdateAsync(ITelegramBotClient bot, Update update, CancellationToken ct)
{
    if (update.CallbackQuery is { Message: { } callbackMessage, Data: { } callbackData })
    {
        long chatId = callbackMessage.Chat.Id;
        await bot.AnswerCallbackQuery(update.CallbackQuery.Id, cancellationToken: ct);
        if (callbackData == "btn_rules")
        {
            await bot.SendMessage(chatId, BotResponses.GetRulesResponse(), cancellationToken: ct);
        }
        else if (callbackData == "btn_links")
        {
            await bot.SendMessage(chatId, BotResponses.GetLinksResponse(), cancellationToken: ct);
        }
        return;
    }
    if (update.Message is not { } message) return;
    if (message.Text is not { } messageText) return;
    long textChatId = message.Chat.Id;
    var inlineKeyboard = new InlineKeyboardMarkup(
       InlineKeyboardButton.WithCallbackData("📜 Munich Rules", "btn_rules"),
       InlineKeyboardButton.WithCallbackData("🔗 Websites and Links", "btn_links")
   );
    switch (messageText)
    {
        case "/start":
            await bot.SendMessage(textChatId, BotResponses.GetHelloResponse(), replyMarkup: inlineKeyboard, cancellationToken: ct);
            break;

        default:
            await bot.SendMessage(textChatId, BotResponses.GetErrorResponse(), cancellationToken: ct);
            break;
    }

    Console.WriteLine("Message received!");
}
async Task HandleErrorAsync(ITelegramBotClient bot, Exception ex, CancellationToken ct)
{
    if (ex is ApiRequestException apiRequestException)
    {
        Console.WriteLine($"[Telegram API Error]: Code {apiRequestException.ErrorCode} — {apiRequestException.Message}");
    }
    else
    {
        Console.WriteLine($"[System Error]: {ex.Message}");
    }
}
class BotResponses
{
    public static string GetRulesResponse()
    {
        return "Important rules in Munich:\n1. Ruhezeit (quiet time) — from 22:00 to 07:00 and the whole day on Sunday. No noise allowed!\n2. Waste sorting — separate plastic, glass and paper, in Bavaria this is taken very seriously.";
    }

    public static string GetLinksResponse()
    {
        return "Useful links:\n1. Official tourist website for Munich: https://www.muenchen.de/int/en.html\n2. Transport information: https://www.mvv-muenchen.de/en/\n3. Events and activities: https://www.muenchen.de/veranstaltungen.html";
    }
    public static string GetHelloResponse()
    {
        return "Hello! I am your guide to Munich. Press the buttons below to learn about the rules and useful links.";
    }
    public static string GetErrorResponse()
    {
        return "Sorry, I don't understand this command.";
    }
}

