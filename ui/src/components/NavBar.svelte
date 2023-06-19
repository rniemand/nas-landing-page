<style>
  ul {
    list-style-type: none;
    margin: 0;
    padding: 0;
    overflow: hidden;
    background-color: #333;
    position: fixed;
    top: 0;
    width: 100%;
    display: block;
  }
  li {
    float: left;
    border-right: 1px solid #bbb;
  }
  li a {
    display: block;
    color: white;
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
  }
  li a:hover { background-color: #111; }
  .active { background-color: #04AA6D; }
  li:last-child { border-right: none; }

  .logout { float: right; background-color: rgb(145, 10, 39); }
  .logout a:hover { background-color: rgb(131, 7, 34); }
</style>

<script lang="ts">
  import {navigating} from '$app/stores';
  import { AuthClient } from '../nlp-api';

  let path = (window.location.pathname || '/').toLowerCase();
  let signedIn = false;
  $: path = (($navigating && $navigating.to?.url.pathname) || path).toLowerCase();

  const runLogout = () => {
    signedIn = false;
    new AuthClient().logout().then(() => {
      location.href = '/';
    });
  };

  (async () => {
    const resp = await new AuthClient().challenge(false);
    signedIn = resp.signedIn || false;
  })();
</script>

<ul>
  {#if signedIn}
    <li><a class="nav-link" href="/" class:active={path==='/'}>Home</a></li>
    <li><a class="nav-link" href="/github" class:active={path==='/github'}>GitHub</a></li>
    <li><a class="nav-link" href="/games" class:active={path==='/games'}>Games</a></li>
    <li><a class="nav-link" href="/tasks" class:active={path==='/tasks'}>Tasks</a></li>
    <li class="logout"><a class="nav-link" href="javascript:void 0" on:click={runLogout}>Logout</a></li>
  {:else}
    <li><a class="nav-link" href="/login" class:active={path==='/login'}>Login</a></li>
  {/if}
</ul>
