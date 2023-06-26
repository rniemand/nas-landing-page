<style>
  ul {
    list-style-type: none;
    margin: 0;
    padding: 0;
    overflow: hidden;
    background-color: #0a1067;
    width: 100%;
    display: inline-block;
    border-radius: 7px;
  }
  li {
    float: left;
    border-right: 1px solid #bbb;
    display: block;
    color: white;
    text-align: center;
    padding: 14px 16px;
    text-decoration: none;
    cursor: pointer;
  }
  li:hover { background-color: #001fa8; }
  .selected { background-color: #2a37f3; }
  li:last-child { border-right: none; }
</style>

<script lang="ts">
  import type { GamePlatformDto } from "../../nlp-api";

  export let platforms: GamePlatformDto[];
  export let selectedPlatform: GamePlatformDto | undefined;
  export let onPlatformSelected: (platform: GamePlatformDto) => void;
</script>

{#if platforms}
  <ul>
    {#each platforms as platform}
      <!-- svelte-ignore a11y-click-events-have-key-events -->
      <li class:selected={platform.platformID === selectedPlatform?.platformID} on:click={() => onPlatformSelected(platform)}>
        {platform.platformName}
      </li>
    {/each}
  </ul>
{/if}
