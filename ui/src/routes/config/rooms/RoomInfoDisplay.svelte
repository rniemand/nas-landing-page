<script lang="ts">
	import { Button } from 'sveltestrap';
	import type { HomeRoomDto } from '../../../nlp-api';

	export let room: HomeRoomDto;
	export let onEditRoom: (room: HomeRoomDto) => void;
	export let onViewFloor: (room: HomeRoomDto) => void;
</script>

<div class="d-flex d-sm-block text-sm-end mt-2">
	<Button class="flex-fill me-1" color="warning" on:click={() => onEditRoom(room)}>
		<i class="bi bi-pencil-square" />
		<span class="d-none d-sm-inline">Edit</span>
	</Button>
	<Button class="flex-fill" color="success" on:click={() => onViewFloor(room)}>
		<i class="bi bi-search" />
		<span class="d-none d-sm-inline">Floor</span>
	</Button>
</div>
