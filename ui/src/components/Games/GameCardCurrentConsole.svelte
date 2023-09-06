<style>
  .console { cursor: pointer; display: flex; font-size: 0.9em; }
  .console:hover { color: rgb(64, 178, 223); }
  .console i { font-size: 1.5em; }
  .console span { text-align: right; flex: auto; }
</style>

<script lang="ts">
  import type { BasicGameInfoDto } from "../../nlp-api";
  export let onClick: () => void;
  export let game: BasicGameInfoDto;
</script>

{#if !game.gameSold}
  <!-- svelte-ignore a11y-click-events-have-key-events -->
  <div class="console" on:click={onClick}>
    <i class="bi bi-controller"></i>
    <span>{game.locationName}</span>
  </div>
{/if}