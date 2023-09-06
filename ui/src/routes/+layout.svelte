<script>
  import 'bootstrap-icons/font/bootstrap-icons.min.css';
  import '../app.css'
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
<div class="container mx-auto">
  <slot>&nbsp;</slot>
</div>