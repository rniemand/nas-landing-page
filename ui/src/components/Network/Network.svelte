<script lang="ts">
  import { NetworkClient, NetworkDeviceDto } from "../../nlp-api";
  import AddDevice from "./AddDevice.svelte";
  import IPv4Info from "./IPv4Info.svelte";
  
  let devices: NetworkDeviceDto[] = [];
  const refreshDevices = async () => {
    devices = await new NetworkClient().getAllDevices();
  };

  refreshDevices();
</script>

<div class="mb-3">
  <AddDevice />
</div>

<table class="table table-striped table-hover table-bordered table-sm">
  <thead>
    <tr>
      <th scope="col">Name</th>
      <th scope="col">Physical</th>
      <th scope="col">Floor</th>
      <th scope="col">Room</th>
      <th scope="col">Classification</th>
      <th scope="col">Manufacturer</th>
      <th scope="col">Model</th>
      <th scope="col">IPv4</th>
    </tr>
  </thead>
  <tbody>
    {#each devices as device}
      <tr>
        <th>{device.deviceName}</th>
        <td>{device.isPhysical}</td>
        <td>{#if device.floor}{device.floor}{/if}</td>
        <td>
          {#if device.room}{device.room}{/if}
          {#if device.roomLocation}({device.roomLocation}){/if}
        </td>
        <td>
          {device.classification.category}
          {#if device.classification.subCategory}
            - {device.classification.subCategory}
          {/if}
        </td>
        <td>
          {#if device.classification.manufacturer}
            {device.classification.manufacturer}
          {/if}
        </td>
        <td>
          {#if device.classification.model}
            {device.classification.model}
          {/if}
        </td>
        <td>
          {#each device.ipv4 as entry}
            <IPv4Info info={entry} />
          {/each}
        </td>
      </tr>
    {/each}
  </tbody>
</table>