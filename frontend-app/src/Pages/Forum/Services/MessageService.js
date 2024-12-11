import { sendMethod } from "../../../Constants/SignalRMethods";
import forumStore from "../../../Services/Forum/ForumStore";

export default async function SendMessageAsync(author, message) {
  await forumStore.connection.invoke(sendMethod, author, message, new Date());
}
