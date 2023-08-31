<style>
  .fix { right: 5px; }
</style>

<script lang="ts">
	import { goto } from "$app/navigation";
  import { navigating } from "$app/stores";
  import { AuthClient, WhoAmIResponse } from "../nlp-api";
	import { authContext, updateAuthContext } from "../utils/AppStore";

  let path = (window.location.pathname || '/').toLowerCase();
  let showAccountDropdown: boolean = false;
  let loggedIn: boolean = false;
  let expanded: boolean = false;

  const runLogout = async () => {
    await new AuthClient().logout();
    updateAuthContext(undefined);
    goto('/');
  };

  const toggleNavBar = () => expanded = !expanded;

  authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
    loggedIn = _whoAmI?.signedIn || false;
  });

  $: path = (($navigating && $navigating.to?.url.pathname) || path).toLowerCase();
</script>

<nav class="navbar bg-dark navbar-expand-lg bg-body-tertiary" data-bs-theme="dark">
  <div class="container-fluid">
    <a class="navbar-brand" href="#!">NLP</a>
    <button class="navbar-toggler" class:collapsed={!expanded} on:click={toggleNavBar} type="button">
      <span class="navbar-toggler-icon"></span>
    </button>
    <div class="collapse navbar-collapse" class:show={expanded} id="navbarSupportedContent">
      <ul class="navbar-nav me-auto mb-2 mb-lg-0">
        <li class="nav-item">
          <a class="nav-link" href="/" on:click={toggleNavBar} class:active={path === '/'}>Home</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="/github" on:click={toggleNavBar} class:active={path === '/github'}>GitHub</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="/games" on:click={toggleNavBar} class:active={path === '/games'}>Games</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="/tasks" on:click={toggleNavBar} class:active={path === '/tasks'}>Tasks</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="/network" on:click={toggleNavBar} class:active={path === '/network'}>Network</a>
        </li>
        <li class="nav-item">
          <a class="nav-link" href="/containers" on:click={toggleNavBar} class:active={path === '/containers'}>Containers</a>
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