<style>
  .platforms { text-align: center; }
</style>

<script lang="ts">
  import type { GamePlatformDto } from "../../nlp-api";
  export let platforms: GamePlatformDto[];
  export let selectedPlatform: GamePlatformDto | undefined;
  export let onPlatformSelected: (platform: GamePlatformDto) => void;
</script>

{#if platforms}
  <div class="mt-2 platforms">
    <ul class="menu menu-vertical lg:menu-horizontal bg-base-200 rounded-box">
      {#each platforms as platform}
        <li class="nav-item">
          <a href="#!" class:active={platform.platformID === selectedPlatform?.platformID} on:click={() => onPlatformSelected(platform)}>
            {platform.platformName}
          </a>
        </li>
      {/each}
    </ul>
  </div>
{/if}