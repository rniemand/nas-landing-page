<style>
  .info { display: block; }
  .mac { font-size: 0.8em; color: red; }
  .ip { font-size: 0.85em; color: blue; }
  .conn { font-size: 0.8em; font-weight: bold; color: rgb(12, 160, 31); }
  .net { font-size: 0.8em; color: rgb(20, 189, 43); }
</style>

<script lang="ts">
  import type { NetworkDeviceIPv4EntryDto } from "../../nlp-api";
  export let info: NetworkDeviceIPv4EntryDto;
</script>

<div class="info">
  <span class="mac">{info.macAddress}</span>
  <span class="ip">{info.ipv4}</span>
  <span class="conn">{info.connection}</span>
  {#if info.networkName}<span class="net">{info.networkName}</span>{/if}
</div>