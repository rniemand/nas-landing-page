<script lang="ts">
	import { FloorClient, HomeFloorDto } from '../../nlp-api';
	import { Input } from 'sveltestrap';

	export let value: number = -1;
	export let className: string = '';
	export let allOption: boolean = false;

	let loading: boolean = true;
	let floors: HomeFloorDto[] = [];
	value = allOption ? (value === -1 ? 0 : value) : value;

	const refreshFloors = async () => {
		loading = true;
		floors = (await new FloorClient().listFloors()) || [];
		if (floors.length > 0 && value <= 0 && !allOption) value = floors[0].floorId;
		loading = false;
	};

	refreshFloors();
</script>

<Input type="select" disabled={loading} bind:value class={className} on:change>
	{#if allOption}<option value={0}>All Floors</option>{/if}
	{#each floors as floor}
		<option value={floor.floorId} selected={value === floor.floorId}>{floor.floorName}</option>
	{/each}
</Input>
