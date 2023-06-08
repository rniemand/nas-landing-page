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
</style>

<script lang="ts">
  import {navigating} from '$app/stores';
  let path = (window.location.pathname || '/').toLowerCase();
  $: path = (($navigating && $navigating.to?.url.pathname) || path).toLowerCase();
</script>

<ul>
  <li><a class="nav-link" href="/" class:active={path==='/'}>Home</a></li>
  <li><a class="nav-link" href="/github" class:active={path==='/github'}>GitHub</a></li>
  <li><a class="nav-link" href="/tasks" class:active={path==='/tasks'}>Tasks</a></li>
</ul>
