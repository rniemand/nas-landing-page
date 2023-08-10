<script lang="ts">
  import UserLinks from "../components/UserLinks/UserLinks.svelte";
	import type { WhoAmIResponse } from "../nlp-api";
  import { authContext } from "../utils/AppStore";

  let loggedIn = true;

  authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
    loggedIn = _whoAmI?.signedIn || false;
  });
</script>

{#if !loggedIn}
  Loading...
{:else}
  <UserLinks />
{/if}
