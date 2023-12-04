import { SocialNetwork } from "./SocialNetwork";

export interface Speaker {
  id: number;
  name: string;
  resume: string;
  imagemURL: string;
  phone: string;
  email: string;
  socialMedia: SocialNetwork[];
}
