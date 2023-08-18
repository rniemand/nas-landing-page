<style>
  .bi { cursor: pointer; }
</style>

<script lang="ts">
  import { NetworkClient, NetworkDeviceDto } from "../../nlp-api";
  import AddClassificationModal from "./AddClassificationModal.svelte";
  import AddDevice from "./AddDevice.svelte";
	import AddIPv4AddressModal from "./AddIPv4AddressModal.svelte";
  import IPv4Info from "./IPv4Info.svelte";
  
  let devices: NetworkDeviceDto[] = [];
  let addClassificationModal: AddClassificationModal;
  let addIPv4AddressModal: AddIPv4AddressModal;

  const refreshDevices = async () => {
    devices = await new NetworkClient().getAllDevices();
  };

  refreshDevices();
</script>

<div class="mb-3">
  <button class="btn btn-success" on:click={refreshDevices}>Refresh</button>
  <AddDevice />
  <AddClassificationModal bind:this={addClassificationModal} />
  <AddIPv4AddressModal bind:this={addIPv4AddressModal} />
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
      <th scope="col">Add</th>
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
          {#if device.classification}
            {device.classification.category}
            {#if device.classification.subCategory}
              - {device.classification.subCategory}
            {/if}
          {/if}
        </td>
        <td>
          {#if device.classification?.manufacturer}
            {device.classification.manufacturer}
          {/if}
        </td>
        <td>
          {#if device.classification?.model}
            {device.classification.model}
          {/if}
        </td>
        <td>
          {#each device.ipv4 as entry}
            <IPv4Info info={entry} />
          {/each}
        </td>
        <td>
          {#if !device.classification}
            <!-- svelte-ignore a11y-click-events-have-key-events -->
            <i class="bi bi-tag-fill" on:click={() => addClassificationModal.open(device)}></i>
          {/if}
          <!-- svelte-ignore a11y-click-events-have-key-events -->
          <i class="bi bi-ethernet" on:click={() => addIPv4AddressModal.open(device)}></i>
        </td>
      </tr>
    {/each}
  </tbody>
</table>