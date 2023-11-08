<script lang="ts">
	import { Button } from 'sveltestrap';
	import type { HomeFloorDto } from '../../../nlp-api';

	export let floor: HomeFloorDto;
	export let onEdit: (floor: HomeFloorDto) => void;
	export let onViewRooms: (floor: HomeFloorDto) => void;
</script>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button color="warning" class="flex-fill me-1" on:click={() => onEdit(floor)}>
		<i class="bi bi-pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<Button color="success" class="flex-fill" on:click={() => onViewRooms(floor)}>
		<i class="bi bi-search" />
		<span class="d-none d-sm-inline">Rooms</span>
	</Button>
</div>
