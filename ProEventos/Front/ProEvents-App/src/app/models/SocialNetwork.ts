import { Speaker } from "./Speaker";

export interface SocialNetwork {
  id: number;
  name: string;
  uRL: string;
  eventId ?: number;
  speakerId ?: number;
}
