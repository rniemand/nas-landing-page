<style>
  .app-wrapper {
    padding-top: 45px;
  }
</style>

<script>
  import NavBar from "../components/NavBar.svelte";
  import 'bootstrap/dist/css/bootstrap.min.css';
  import 'bootstrap-icons/font/bootstrap-icons.min.css';
  import TopNavigation from "../components/TopNavigation.svelte";
  import { AuthClient } from "../nlp-api";
	import { updateAuthContext } from "../utils/AppStore";
	import { goto } from "$app/navigation";

  (async () => {
    const authResponse = await new AuthClient().challenge(false);
    updateAuthContext(authResponse);
    if(!authResponse?.signedIn) goto('/login');
  })();
</script>

<TopNavigation />
<!-- <NavBar /> -->
<div class="app-wrapper">
  <slot />
</div>