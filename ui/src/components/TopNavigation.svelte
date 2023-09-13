<style>
  .fix { right: 5px; }
</style>

<script lang="ts">
	import { goto } from "$app/navigation";
  import { navigating } from "$app/stores";
  import { AuthClient, WhoAmIResponse } from "../nlp-api";
	import { authContext, updateAuthContext } from "../utils/AppStore";
	import NavLinkItem from "./NavLinkItem.svelte";
	import type { NavbarLink } from "./TopNavigation";

  let path = (window.location.pathname || '/').toLowerCase();
  let loggedIn: boolean = false;
  let expanded: boolean = false;

  const runLogout = async () => {
    await new AuthClient().logout();
    updateAuthContext(undefined);
    goto('/');
  };

  let links: NavbarLink[] = [];

  const toggleNavBar = () => expanded = !expanded;

  const generateNavLinks = (_loggedIn: boolean) => {
    if(!_loggedIn) return [];

    return [
      {
        text: 'GitHub',
        href: '/github'
      },
      {
        text: 'Games',
        href: '/games'
      },
      {
        text: 'Tasks',
        href: '/tasks'
      },
      {
        text: 'Network',
        href: '/network'
      },
      {
        text: 'Containers',
        href: '/containers'
      },
      {
        text: 'Account',
        children: [
          {
            text: 'Sign Out',
            onClick: runLogout
          }
        ]
      }
    ];
  };

  authContext.subscribe((_whoAmI: WhoAmIResponse | undefined) => {
    loggedIn = _whoAmI?.signedIn || false;
    links = generateNavLinks(loggedIn);
  });

  $: path = (($navigating && $navigating.to?.url.pathname) || path).toLowerCase();
</script>

<div class="navbar bg-neutral">
  <div class="navbar-start">
    <div class="dropdown">
      <label tabindex="0" class="btn btn-ghost lg:hidden">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-5 w-5" fill="none" viewBox="0 0 24 24" stroke="currentColor"><path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h8m-8 6h16" /></svg>
      </label>
      <ul class="menu menu-lg dropdown-content mt-3 z-[30] p-2 shadow bg-base-100 rounded-box w-52">
        {#each links as link}<NavLinkItem {link} />{/each}
      </ul>
    </div>
    <a class="btn btn-ghost normal-case text-xl" href="/">NLP</a>
  </div>
  <div class="navbar-end hidden lg:flex">
    <ul class="menu menu-horizontal px-1">
      {#each links as link}<NavLinkItem {link} />{/each}
    </ul>
  </div>
</div>