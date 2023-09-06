<style>
	.link { cursor: pointer; }
	/* .card { background-color: #ffffff; }
	.card:hover { background-color: #e0e0e0; } */
	.link img {
		max-width: 100%;
		height: auto;
		border: 1px solid #e1e1e1;
		padding: 3px;
		border-radius: 4px;
		background-color: #fff;
		margin: 6px;
	}
	.d-flex { display: flex; }
	.card-actions span { flex: auto; text-align: center; }
	.follow-count { color: #0d970d; }
	.order { color: #8c8c8d; }
	.card {
		margin-right: 6px;
		margin-bottom: 12px;
		width: 50%;
		min-width: 100px;
		max-width: 150px;
		cursor: pointer;
	}
	.card:hover { background-color: rgb(7, 10, 12); }
</style>

<script lang="ts">
	import { UserLinksClient, type UserLinkDto } from '../../nlp-api';
	export let link: UserLinkDto;

	const handleClick = () => {
		new UserLinksClient().recordLinkFollow(link.linkID).then((success: boolean) => {
			if(success) link.followCount += 1;
		});
		window.open(link.linkUrl, '_blank');
	};
</script>

<!-- svelte-ignore a11y-click-events-have-key-events -->
<div class="card card-compact image-full bg-base-100 shadow-xl" on:click={handleClick}>
	<figure><img src={`api/images/link/${link.linkImage}`} alt={link.linkName} /></figure>
	<div class="card-body text-ellipsis overflow-hidden">
		{link.linkName}
		<div class="card-actions justify-end">
			<span class="id"># {link.linkID}</span>
			<span class="follow-count">{link.followCount}</span>
			<span class="order">{link.linkOrder}</span>
		</div>
	</div>
</div>