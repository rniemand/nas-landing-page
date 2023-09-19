<script lang="ts">
	import { goto } from "$app/navigation";
  import { navigating } from "$app/stores";
  import { AuthClient, WhoAmIResponse } from "../nlp-api";
	import { authContext, updateAuthContext } from "../utils/AppStore";

  let path = (window.location.pathname || '/').toLowerCase();
  let loggedIn: boolean = false;

  const runLogout = async () => {
    await new AuthClient().logout();
    updateAuthContext(undefined);
    goto('/');
  };

  authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
    loggedIn = _whoAmI?.signedIn || false;
  });

  $: path = (($navigating && $navigating.to?.url.pathname) || path).toLowerCase();
</script>

<nav class="navbar navbar-expand-lg navbar-dark bg-dark" aria-label="Offcanvas navbar large">
  <div class="container-fluid">
    <a class="navbar-brand" href="/">NLP</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNavbar2" aria-controls="offcanvasNavbar2" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="offcanvas offcanvas-end text-bg-dark" tabindex="-1" id="offcanvasNavbar2" aria-labelledby="offcanvasNavbar2Label">
      <div class="offcanvas-header">
        <h5 class="offcanvas-title" id="offcanvasNavbar2Label">Offcanvas</h5>
        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="offcanvas" aria-label="Close"></button>
      </div>
      <div class="offcanvas-body">
        <ul class="navbar-nav justify-content-end flex-grow-1 pe-3">
          {#if loggedIn}
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="/github">GitHub</a>
            </li>
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="/games">Games</a>
            </li>
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="/tasks">Tasks</a>
            </li>
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="/network">Network</a>
            </li>
            <li class="nav-item">
              <a class="nav-link active" aria-current="page" href="/containers">Containers</a>
            </li>
            <li class="nav-item dropdown dropstart">
              <a class="nav-link dropdown-toggle" href="#!" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                Account
              </a>
              <ul class="dropdown-menu">
                <li><a class="dropdown-item" href="#!" on:click={runLogout}>Sign Out</a></li>
              </ul>
            </li>
          {/if}
        </ul>
      </div>
    </div>
  </div>
</nav>