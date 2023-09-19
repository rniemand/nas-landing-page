<script lang="ts">
  import UserLinks from "../components/UserLinks/UserLinks.svelte";
	import type { WhoAmIResponse } from "../nlp-api";
  import { authContext } from "../utils/AppStore";

  let loggedIn = true;

  authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
    loggedIn = _whoAmI?.signedIn || false;
  });
</script>

<div class="alert alert-warning" role="alert">
  This is a work in progress!
</div>

<div class="container text-center">
  {#if loggedIn}
    <UserLinks />
  {/if}
</div>