<style>
	.description {
		border: 1px solid #3f3f3f;
		background-color: #161616;
		border-radius: 0.5em;
	}
</style>

<script lang="ts">
	import { Button } from 'sveltestrap';
	import type { HomeChoreDto } from '../../../nlp-api';
	import moment from 'moment';
	import ChoreIntervalDisplay from './ChoreIntervalDisplay.svelte';

	export let chore: HomeChoreDto;
	export let onEditChore: (chore: HomeChoreDto) => void;
</script>

<div class="description p-2">{chore.choreDescription}</div>

<div class="info">
	<i class="bi bi-alarm" />
	{moment(chore.dateScheduled).fromNow()}
</div>
<div class="info">
	<i class="bi bi-currency-dollar" />
	{chore.chorePoints} point(s)
</div>
<div class="info">
	<i class="bi bi-geo" />
	{chore.floorName} / {chore.roomName}
</div>
<div class="info">
	<i class="bi bi-journal-check" />
	{chore.completedCount}
</div>
<div class="info">
	<i class="bi bi-calendar2-week-fill" />
	<ChoreIntervalDisplay {chore} />
</div>

<div class="text-end">
	<Button color="primary" on:click={() => onEditChore(chore)}>Edit</Button>
</div>
