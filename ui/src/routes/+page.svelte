<script lang="ts">
  import UserLinks from "../components/UserLinks/UserLinks.svelte";
  import { AuthClient } from "../nlp-api";

  let loading = true;

  (async () => {
    const resp = await new AuthClient().challenge(false);
    if(!resp.signedIn) {
      setTimeout(() => { location.href = '/login'; }, 100);
      return;
    }
    loading = false;
  })();
</script>

{#if loading}
  Loading...
{:else}
  <UserLinks />
{/if}
