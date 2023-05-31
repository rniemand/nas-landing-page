import type { UserLinkDto } from "../nlp-api";

export interface LinkCategory {
  name: string;
  links: UserLinkDto[];
}