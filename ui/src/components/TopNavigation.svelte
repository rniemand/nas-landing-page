<style>
  .fix { right: 5px; }
</style>

<script lang="ts">
  import { navigating } from "$app/stores";
  import { AuthClient, WhoAmIResponse } from "../nlp-api";
	import { authContext, updateAuthContext } from "../utils/AppStore";

  let path = (window.location.pathname || '/').toLowerCase();
  let showAccountDropdown: boolean = false;
  let loggedIn: boolean = false;

  const runLogout = () => {
    new AuthClient().logout().then(() => {
      updateAuthContext(undefined);
      location.href = '/';
    });
  };

  authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
    loggedIn = _whoAmI?.signedIn || false;
  });

  $: path = (($navigating && $navigating.to?.url.pathname) || path).toLowerCase();
</script>

<nav class="navbar bg-dark navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
  <div class="container-fluid">
    <a class="navbar-brand" href="#!">NLP</a>
    <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link active" href="/" class:active={path === '/'}>Home</a>
        </li>
        <li class="nav-item">
          <a class="nav-link active" href="/github" class:active={path === '/github'}>GitHub</a>
        </li>
        <li class="nav-item">
          <a class="nav-link active" href="/games" class:active={path === '/games'}>Games</a>
        </li>
        <li class="nav-item">
          <a class="nav-link active" href="/tasks" class:active={path === '/tasks'}>Tasks</a>
        </li>
      </ul>
      <div class="d-flex">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          {#if loggedIn}
            <li class="nav-item dropdown">
              <a class="nav-link dropdown-toggle" href="#!" role="button" class:show={showAccountDropdown} on:click={() => showAccountDropdown = !showAccountDropdown}>
                Account
              </a>
              <ul class="dropdown-menu fix" class:show={showAccountDropdown}>
                <li><a class="dropdown-item" href="#!">Action</a></li>
                <li><hr class="dropdown-divider"></li>
                <li><a class="dropdown-item" href="#!" on:click={runLogout}>
                  <i class="bi bi-lock-fill"></i> Sign Out
                </a></li>
              </ul>
            </li>
          {/if}
        </ul>
      </div>
    </div>
  </div>
</nav>