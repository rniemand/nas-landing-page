import { writable, type Writable } from "svelte/store";
import type { WhoAmIResponse } from "../nlp-api";

export const authContext: Writable<WhoAmIResponse | undefined> = writable();

export const updateAuthContext = (context: WhoAmIResponse | undefined) => {
  authContext.update((_old) => context);
};
