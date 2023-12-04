import { Batch } from "./Batch";
import { SocialNetwork } from "./SocialNetwork";
import { Speaker } from "./Speaker";

export interface Event {
  id: number;
  local: string;
  eventDate ?: Date
  theme: string;
  amountPeople: number;
  imageURL: string;
  phone: string;
  email: string;
  batches: Batch[];
  socialMedia: SocialNetwork[];
  eventSpeakers: Speaker[];
}
