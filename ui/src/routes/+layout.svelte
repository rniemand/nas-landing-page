<script>
  import NavBar from "../components/NavBar.svelte";
</script>

<style>
  .app-wrapper {
    padding-top: 45px;
  }
</style>

<NavBar />
<div class="app-wrapper">
  <slot />
</div>