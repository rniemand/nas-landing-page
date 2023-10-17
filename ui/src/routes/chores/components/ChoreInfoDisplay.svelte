<script lang="ts">
	import { Button } from 'sveltestrap';
	import type { HomeChoreDto } from '../../../nlp-api';
	import moment from 'moment';

	export let chore: HomeChoreDto;
	export let onEditChore: (chore: HomeChoreDto) => void;
</script>

<div>
	name: {chore.choreName}
</div>
<div>
	description: {chore.choreDescription}
</div>
<div>
	next occurance: {moment(chore.dateScheduled).format('yyyy-MM-DD')}
</div>
<div class="bob">
	raw occurance: {chore.dateScheduled}
</div>
<div>
	from now: {moment(chore.dateScheduled).fromNow()}
</div>
<div>
	points: {chore.chorePoints} points
</div>
<div>
	floor: {chore.floorName}
</div>
<div>
	room: {chore.roomName}
</div>
<div>
	complete count: {chore.completedCount}
</div>
<div>
	priority: {chore.priority}
</div>
<div>
	interval: {chore.intervalModifier} | {chore.interval}
</div>
<div class="text-end">
	<Button color="primary" on:click={() => onEditChore(chore)}>Edit</Button>
</div>
